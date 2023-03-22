namespace Elephant.Types.Operators
{
    /// <summary>
    /// Bitwise operators.
    /// </summary>
    public enum Bitwises
    {
        /// <summary>
        /// No operator.
        /// </summary>
        None,

        /// <summary>
        /// <![CDATA[&]]>. Copies a bit to the result if it exists in both operands.
        /// </summary>
        And,

        /// <summary>
        /// |. Copies a bit if it exists in either operand.
        /// </summary>
        Or,

        /// <summary>
        /// ^. Copies the bit if it is set in one operand but not both.
        /// </summary>
        Xor,

        /// <summary>
        /// ~. Is unary and has the effect of 'flipping' bits.
        /// </summary>
        Complement,

        /// <summary>
        /// <![CDATA[<<]]>. The left operands value is moved left by the number of bits specified by the right operand.
        /// </summary>
        ShiftLeft,

        /// <summary>
        /// >>. The left operands value is moved right by the number of bits specified by the right operand.
        /// </summary>
        ShiftRight,
    }
}
