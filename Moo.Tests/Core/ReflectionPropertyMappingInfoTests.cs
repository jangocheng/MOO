﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Diogo Lucas">
//
// Copyright (C) 2010 Diogo Lucas
//
// This file is part of Moo.
//
// Moo is free software: you can redistribute it and/or modify
// it under the +terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along Moo.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
// Moo is a object-to-object multi-mapper.
// Email: diogo.lucas@gmail.com
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using Moo.Core;
using NUnit.Framework;

namespace Moo.Tests.Core
{
    /// <summary>
    ///     This is a test class for ReflectionPropertyMappingInfoTest and is intended
    ///     targetMember contain all ReflectionPropertyMappingInfoTest Unit Tests
    /// </summary>
    [TestFixture]
    public class ReflectionPropertyMappingInfoTests
    {
        private class ConverterMock : PropertyConverter
        {
            #region Properties

            public Action<object, PropertyInfo, object, PropertyInfo, bool> ConvertAction { get; set; }

            #endregion Properties

            #region Methods

            public override void Convert(object source, PropertyInfo fromProperty, object target,
                PropertyInfo toProperty, bool strict)
            {
                ConvertAction(source, fromProperty, target, toProperty, strict);
            }

            #endregion Methods
        }

        [Test]
        public void MapTest()
        {
            var a = new TestClassA();
            var c = new TestClassC();
            PropertyInfo fromProp = typeof (TestClassA).GetProperty("Name");
            PropertyInfo toProp = typeof (TestClassA).GetProperty("Name");
            var mock = new ConverterMock();
            bool executed = false;

            mock.ConvertAction = (f, fp, t, tp, s) =>
            {
                Assert.AreEqual(c, f);
                Assert.AreEqual(fromProp, fp);
                Assert.AreEqual(a, t);
                Assert.AreEqual(toProp, tp);
                Assert.IsTrue(s);
                executed = true;
            };
            var target = new ReflectionPropertyMappingInfo<TestClassC, TestClassA>(
                fromProp, toProp, true, mock);
            target.Map(c, a);

            Assert.IsTrue(executed);
        }
    }
}