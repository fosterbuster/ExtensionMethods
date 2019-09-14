// <copyright file="MBusExtensions.cs" company="Poul Erik Venø Hansen">
// Copyright (c) Poul Erik Venø Hansen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace FosterBuster.Extensions
{
    /// <summary>
    /// MBus specific extension methods.
    /// </summary>
    public static class MBusExtensions
    {
        /// <summary>
        /// Gets an integer representation of a manufacturer code string.
        /// </summary>
        /// <param name="manId">the manufacturer code string.</param>
        /// <returns>integer representation of <paramref name="manId"/>.</returns>
        public static int ToManufacturerCode(this string manId)
        {
            if (manId.Length != 3)
            {
                throw new ArgumentException("Manufacturer Name must no more and no less than 3 letters");
            }

            return ((manId[0] - 64) * 1024) + ((manId[1] - 64) * 32) + (manId[2] - 64);
        }

        /// <summary>
        /// Gets the manufacturer name.
        /// </summary>
        /// <param name="manId">the 2-byte manufacturer value.</param>
        /// <returns>the manufacturer name.</returns>
        public static string ToManufacturerName(this short manId)
        {
            return new string(
               new[]
               {
                    (char)((manId / 1024) + 64),
                    (char)(((manId % 1024) / 32) + 64),
                    (char)((manId % 32) + 64),
               });
        }

        /// <summary>
        /// Gets the manufacturer name.
        /// </summary>
        /// <param name="manId">the 2-byte manufacturer value.</param>
        /// <returns>the manufacturer name.</returns>
        public static string ToManufacturerName(this int manId)
        {
            return ((short)manId).ToManufacturerName();
        }

        /// <summary>
        /// Gets a value indicating whether the byte has the extension-bit set or not.
        /// </summary>
        /// <param name="b">the byte.</param>
        /// <returns>if the byte has the extension bit set.</returns>
        public static bool HasExtensionBit(this byte b)
        {
            return b.Mask(0b1000_0000) == 0b1000_0000;
        }
    }
}