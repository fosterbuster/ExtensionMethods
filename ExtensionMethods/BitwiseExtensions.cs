// <copyright file="BitwiseExtensions.cs" company="Poul Erik Venø Hansen">
// Copyright (c) Poul Erik Venø Hansen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace FosterBuster.Extensions
{
    /// <summary>
    /// General extension methods for byte stuff.
    /// </summary>
    public static class BitwiseExtensions
    {
        /// <summary>
        /// Gets the high nibble of the passed <paramref name="b"/>.
        /// </summary>
        /// <param name="b">the byte.</param>
        /// <returns>the high nibble.</returns>
        public static byte GetHighNibble(this byte b)
        {
            return b.ShiftRight(4).Mask(0b0000_1111);
        }

        /// <summary>
        /// Gets the low nibble of the passed <paramref name="b"/>.
        /// </summary>
        /// <param name="b">the byte.</param>
        /// <returns>the low nibble.</returns>
        public static byte GetLowNibble(this byte b)
        {
            return b.Mask(0b0000_1111);
        }

        /// <summary>
        /// Gets the bit at the <paramref name="position"/>.
        /// </summary>
        /// <param name="b">the byte.</param>
        /// <param name="position">the bit at the given position to extract.</param>
        /// <returns>A boolean indicating if it was 1 or 0.</returns>
        public static bool GetBit(this byte b, int position)
        {
            return (b & (1 << position)) != 0;
        }

        /// <summary>
        /// Sets the bit at the <paramref name="position"/>.
        /// </summary>
        /// <param name="b">the byte to manipulate.</param>
        /// <param name="position">the postion.</param>
        /// <returns>the same byte, but different.</returns>
        public static byte SetBit(this byte b, int position)
        {
            return (byte)(b | (1 << position));
        }

        /// <summary>
        /// Transforms a hex-formatted string to a byte array.
        /// </summary>
        /// <param name="hexString">the string.</param>
        /// <returns>a byte array of the hex-strings binary value.</returns>
        public static byte[] HexStringToBytes(this string hexString)
        {
            var sanitizedHex = hexString.ToUpper();

            if (sanitizedHex.Length % 2 == 1)
            {
                throw new ArgumentException("The binary key cannot have an odd number of digits");
            }

            var arr = new byte[sanitizedHex.Length >> 1];

            for (var i = 0; i < sanitizedHex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexValue(sanitizedHex[i << 1]) << 4) +
                                      GetHexValue(sanitizedHex[(i << 1) + 1]));
            }

            return arr;
        }

        /// <summary>
        /// Transforms a byte to a hex-formatted string.
        /// </summary>
        /// <param name="b">the byte.</param>
        /// <returns><see cref="string"/>.</returns>
        public static string ToHexString(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// Parses the passed <paramref name="bytes"/> into a hex-formatted string.
        /// </summary>
        /// <param name="bytes">The bytes to be parsed.</param>
        /// <returns>A hex formatted string.</returns>
        public static string ToHexString(this IList<byte> bytes)
        {
            var c = new char[bytes.Count * 2];
            for (var i = 0; i < bytes.Count; i++)
            {
                var b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[(i * 2) + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }

            return new string(c);
        }

        /// <summary>
        /// Applies the given <paramref name="mask"/>.
        /// </summary>
        /// <param name="b">the byte to mask.</param>
        /// <param name="mask">the masl.</param>
        /// <returns>the masked byte.</returns>
        public static byte Mask(this byte b, byte mask)
        {
            return (byte)(b & mask);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        /// <param name="b">the byte to OR with <paramref name="other"/>.</param>
        /// <param name="other">the byte to OR with <paramref name="b"/>.</param>
        /// <returns>The result of the OR operation.</returns>
        public static byte Or(this byte b, byte other)
        {
            return (byte)(b | other);
        }

        /// <summary>
        /// Bitwise right-shift.
        /// </summary>
        /// <param name="b">the byte to perform the shift on.</param>
        /// <param name="places">number of bits to shift.</param>
        /// <returns>the shifted byte.</returns>
        public static byte ShiftRight(this byte b, byte places)
        {
            return (byte)(b >> places);
        }

        /// <summary>
        /// Bitwise left-shift.
        /// </summary>
        /// <param name="b">the byte to perform the shift on.</param>
        /// <param name="places">number of bits to shift.</param>
        /// <returns>the shifted byte.</returns>
        public static byte ShiftLeft(this byte b, byte places)
        {
            return (byte)(b << places);
        }

        private static int GetHexValue(char hex)
        {
            var val = (int)hex;

            // For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
        }
    }
}