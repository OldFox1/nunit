﻿// ***********************************************************************
// Copyright (c) 2013 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
using System;
using System.Text;

namespace NUnit.Framework.Internal
{
    /// <summary>
    /// RandomGenerator returns a set of random _values in a repeatable
    /// way, to allow re-running of tests if necessary. 
    /// 
    /// This class is internal to the framework but exposed externally to through the TestContext 
    /// the class is used to allow for obtaining repeatable random _values during a tests execution.
    /// this class should not be used inside the framework only with a TestMethod.
    /// </summary>
    public class RandomGenerator
    {
        #region Members & Constructor
        /// <summary>
        /// Seed for the wrapped Random
        /// </summary>
        public readonly int seed;

        private Random random;

        /// <summary>
        /// Lazy-loaded Random built on the readonly Seed
        /// </summary>
        private Random Rand
        {
            get
            {
                random = random == null ? new Random(seed) : random;
                return random;
            }
        }

        /// <summary>
        /// Constructor requires Seed value in order to store it for use in Random creation
        /// </summary>
        /// <param name="seed"></param>
        public RandomGenerator(int seed)
        {
            this.seed = seed;
        }
        #endregion

        #region Ints
        /// <summary>
        /// Get Next Integer from Random 
        /// </summary>
        /// <returns> int </returns>
        public int GetInt()
        {
            return Rand.Next();
        }
        /// <summary>
        /// Get Next Integer within the specified min &amp; max from Random 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns> int </returns>
        public int GetInt(int min, int max)
        {
            return Rand.Next(min, max);
        }
        #endregion

        #region Shorts
        /// <summary>
        /// Get Next Short from Random
        /// </summary>
        /// <returns> short </returns>
        public short GetShort()
        {
            return (short)Rand.Next(short.MinValue, short.MaxValue);
        }
        /// <summary>
        /// Get Next Short within the specified min &amp; max from Random 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns> short </returns>
        public short GetShort(short min, short max)
        {
            return (short)Rand.Next(min, max);
        }
        #endregion

        #region Bytes
        /// <summary>
        /// Get Next Byte from Random
        /// </summary>
        /// <returns> byte </returns>
        public byte GetByte()
        {
            return (byte)Rand.Next(Byte.MinValue, Byte.MaxValue);
        }
        /// <summary>
        /// Get Next Byte within the specified min &amp; max from Random
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns> byte </returns>
        public byte GetByte(byte min, byte max)
        {
            return (byte)Rand.Next(min, max);
        }
        #endregion

        #region Bools
        /// <summary>
        /// Get Random Boolean value
        /// </summary>
        /// <returns> bool </returns>
        public bool GetBool()
        {
            return Rand.Next(0, 2) == 0;
        }
        /// <summary>
        /// Get Random Boolean value based on the probability of that value being true
        /// </summary>
        /// <param name="probability"></param>
        /// <returns> bool </returns>
        public bool GetBool(double probability)
        {
            return Rand.NextDouble() < Math.Abs(probability % 1.0);
        }
        #endregion

        #region Double & Float
        /// <summary>
        /// Get Next Double from Random
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            return Rand.NextDouble();
        }
        /// <summary>
        /// Get Next Float from Random
        /// </summary>
        /// <returns></returns>
        public float GetFloat()
        {
            return (float)Rand.NextDouble();
        }
        #endregion

        #region Enums

        /// <summary>
        /// Return a random enum value representation of the specified Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns> T </returns>
        public T GetEnum<T>()
        {
            Array enums = TypeHelper.GetEnumValues(typeof(T));
            return (T)enums.GetValue(Rand.Next(0, enums.Length));
        }

        #endregion
        
        #region String
        
        /// <summary>
        /// Default characters for random functions.
        /// </summary>
        /// <remarks>Default characters are the English alphabet (uppercase &amp; lowercase), arabic numerals, and underscore</remarks>
        public const string DefaultStringChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_";

        private const int DefaultStringLength = 25;
                
        /// <summary>
        /// Generate a random string based on the characters from the input string.
        /// </summary>
        /// <param name="outputLength">desired length of output string.</param>
        /// <param name="allowedChars">string representing the set of characters from which to construct the resulting string</param>
        /// <returns>A random string of arbitrary length</returns>
        public string GetString(int outputLength, string allowedChars)
        {

            var sb = new StringBuilder(outputLength);

            for (int i = 0; i < outputLength ; i++)
            {
                sb.Append(allowedChars[GetInt(0,allowedChars.Length)]);
            }
        
            return sb.ToString();
        }

        /// <summary>
        /// Generate a random string based on the characters from the input string.
        /// </summary>
        /// <param name="outputLength">desired length of output string.</param>
        /// <returns>A random string of arbitrary length</returns>
        /// <remarks>Uses <see cref="DefaultStringChars">DefaultStringChars</see> as the input character set </remarks>
        public string GetString(int outputLength)
        {
            return GetString(outputLength, DefaultStringChars);
        }

        /// <summary>
        /// Generate a random string based on the characters from the input string.
        /// </summary>
        /// <returns>A random string of the default length</returns>
        /// <remarks>Uses <see cref="DefaultStringChars">DefaultStringChars</see> as the input character set </remarks>
        public string GetString()
        {
            return GetString(DefaultStringLength, DefaultStringChars);
        }

        #endregion
        
    }
}
