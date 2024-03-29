﻿namespace Elephant.Types.Operators
{
    /// <summary>
    /// Assignment operators.
    /// </summary>
    public enum Assignments
    {
        /// <summary>
        /// No operator.
        /// </summary>
        None,

        /// <summary>
        /// =.
        /// </summary>
        Assign,

        /// <summary>
        /// +=.
        /// </summary>
        Add,

        /// <summary>
        /// -=.
        /// </summary>
        Subtract,

        /// <summary>
        /// *=.
        /// </summary>
        Multiply,

        /// <summary>
        /// /=.
        /// </summary>
        Divide,

        /// <summary>
        /// %=.
        /// </summary>
        Modulus,

        /// <summary>
        /// <![CDATA[<<=. C <<= 2 is same as C = C << 2]]>.
        /// </summary>
        ShiftLeft,

        /// <summary>
        /// >>=. C >>= 2 is same as C = C >> 2.
        /// </summary>
        ShiftRight,

        /// <summary>
        /// <![CDATA[&]]>=.
        /// </summary>
        BitwiseAnd,

        /// <summary>
        /// ^=. Bitwise exclusive or assignment operator.
        /// </summary>
        BitwiseXor,

        /// <summary>
        /// |=. Bitwise inclusive or assignment operator.
        /// </summary>
        BitwiseOr,
    }
}
