using System.Buffers;
using System.Diagnostics;
using System.Globalization;

namespace System.Text.Json
{
    internal abstract class JsonSeparatorNamingPolicy : JsonNamingPolicy
    {
        private readonly bool _lowercase;
        private readonly char _separator;

        internal JsonSeparatorNamingPolicy(bool lowercase, char separator)
        {
            Debug.Assert(char.IsPunctuation(separator));

            _lowercase = lowercase;
            _separator = separator;
        }

        public sealed override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ConvertNameCore(_separator, _lowercase, name.AsSpan());
        }

        private static string ConvertNameCore(char separator, bool lowercase, ReadOnlySpan<char> chars)
        {
            char[]? rentedBuffer = null;

            int initialBufferLength = (int)(1.2 * chars.Length);
            Span<char> destination = initialBufferLength <= JsonConstants.StackallocCharThreshold
                ? stackalloc char[JsonConstants.StackallocCharThreshold]
                : (rentedBuffer = ArrayPool<char>.Shared.Rent(initialBufferLength));

            SeparatorState state = SeparatorState.NotStarted;
            int charsWritten = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                char current = chars[i];
                UnicodeCategory category = char.GetUnicodeCategory(current);

                switch (category)
                {
                    case UnicodeCategory.UppercaseLetter:

                        switch (state)
                        {
                            case SeparatorState.NotStarted:
                                break;

                            case SeparatorState.LowercaseLetterOrDigit:
                            case SeparatorState.SpaceSeparator:
                                WriteChar(separator, ref destination);
                                break;

                            case SeparatorState.UppercaseLetter:
                                if (i + 1 < chars.Length && char.IsLower(chars[i + 1]))
                                {
                                    WriteChar(separator, ref destination);
                                }

                                break;

                            default:
                                Debug.Fail($"Unexpected state {state}");
                                break;
                        }

                        if (lowercase)
                        {
                            current = char.ToLowerInvariant(current);
                        }

                        WriteChar(current, ref destination);
                        state = SeparatorState.UppercaseLetter;
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:

                        if (state is SeparatorState.SpaceSeparator)
                        {
                            // Normalize preceding spaces to one separator.
                            WriteChar(separator, ref destination);
                        }

                        if (!lowercase && category is UnicodeCategory.LowercaseLetter)
                        {
                            current = char.ToUpperInvariant(current);
                        }

                        WriteChar(current, ref destination);
                        state = SeparatorState.LowercaseLetterOrDigit;
                        break;

                    case UnicodeCategory.SpaceSeparator:
                        if (state != SeparatorState.NotStarted)
                        {
                            state = SeparatorState.SpaceSeparator;
                        }

                        break;

                    default:
                        WriteChar(current, ref destination);
                        state = SeparatorState.NotStarted;
                        break;
                }
            }

            string result = destination.Slice(0, charsWritten).ToString();

            if (rentedBuffer != null)
            {
                destination.Slice(0, charsWritten).Clear();
                ArrayPool<char>.Shared.Return(rentedBuffer);
            }

            return result;

            void WriteChar(char value, ref Span<char> destination)
            {
                if (charsWritten == destination.Length)
                {
                    ExpandBuffer(ref destination);
                }

                destination[charsWritten++] = value;
            }

            void ExpandBuffer(ref Span<char> destination)
            {
                int newSize = checked(destination.Length * 2);
                char[] newBuffer = ArrayPool<char>.Shared.Rent(newSize);
                destination.CopyTo(newBuffer);

                if (rentedBuffer != null)
                {
                    destination.Slice(0, charsWritten).Clear();
                    ArrayPool<char>.Shared.Return(rentedBuffer);
                }

                rentedBuffer = newBuffer;
                destination = rentedBuffer;
            }
        }

        private enum SeparatorState
        {
            NotStarted,
            UppercaseLetter,
            LowercaseLetterOrDigit,
            SpaceSeparator,
        }
    }

    public static class JsonConstants
    {
        public const int StackallocByteThreshold = 256;

        public const int StackallocCharThreshold = StackallocByteThreshold / 2;
    }
}