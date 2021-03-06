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

using Moo.Core;
using NUnit.Framework;

namespace Moo.Tests
{
    /// <summary>
    ///     This is a test class for DelegateMappingInfoTest and is intended
    ///     targetMember contain all DelegateMappingInfoTest Unit Tests
    /// </summary>
    [TestFixture]
    public class DelegateMappingInfoTests
    {
        [Test]
        public void MapTest()
        {
            bool executed = false;

            var target = new DelegateMappingInfo<TestClassC, TestClassD>(
                "sourceMemberName",
                "targetMemberName",
                (f, t) => executed = true);
            target.Map(new TestClassC(), new TestClassD());

            Assert.IsTrue(executed);
        }
    }
}