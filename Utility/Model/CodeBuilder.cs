using DevelApp.Utility.Exceptions;
using System.Text;

namespace DevelApp.Utility.Model
{
    /// <summary>
    /// Wrapped StringBuilder to avoid it taking too much attention
    /// </summary>
    public sealed class CodeBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly int _indentSpacesPerIndent;
        private int _indent = 0;

        public CodeBuilder(int indentSpacesPerIndent = 4)
        {
            if (indentSpacesPerIndent < 0)
            {
                throw new CodeBuilderException($"indentSpacesPerIndent cannot be below 0: {indentSpacesPerIndent}");
            }
            _indentSpacesPerIndent = indentSpacesPerIndent;
        }

        /// <summary>
        /// Appends an indented line and returns itself to allow for chained calls
        /// </summary>
        public CodeBuilder L(string line)
        {
            if (_indent > 0)
            {
                _stringBuilder.Append(' ', _indent * _indentSpacesPerIndent);
            }
            _stringBuilder.AppendLine(line);
            return this;
        }

        /// <summary>
        /// Appends an empty line and returns itself to allow for chained calls
        /// </summary>
        public CodeBuilder EmptyLine()
        {
            _stringBuilder.AppendLine();
            return this;
        }

        /// <summary>
        /// Called in the end of the code build to translate builder code into string.
        /// Throws if the indent has not been reset to zero.
        /// </summary>
        public string Build()
        {
            if (_indent != 0)
            {
                throw new CodeBuilderException("Indent has not been reset to zero, indicating an error in generated code");
            }
            return _stringBuilder.ToString();
        }

        /// <summary>
        /// Increase indent for the code
        /// </summary>
        public CodeBuilder IndentIncrease()
        {
            _indent += 1;
            return this;
        }

        /// <summary>
        /// Decrease indent for the code. Throws if the indent is already zero.
        /// </summary>
        public CodeBuilder IndentDecrease()
        {
            if (_indent <= 0)
            {
                throw new CodeBuilderException("Indent cannot be decreased below zero, indicating an error in generated code");
            }
            _indent -= 1;
            return this;
        }
    }
}