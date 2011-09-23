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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Configuration;
using Moo.Mappers;

namespace Moo.Tests.Mappers
{
    /// <summary>
    /// This is a test class for ConfigurationMapperTest and is intended
    /// targetMemberName contain all ConfigurationMapperTest Unit Tests
    /// </summary>
    [TestClass()]
    public class ConfigurationMapperTest
    {
        #region Fields (1)

        private TestContext testContextInstance;

        #endregion Fields

        #region Properties (1)

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 

        [TestMethod()]
        public void GetTypeMappingNoSectionTest()
        {
            var actual = ConfigurationMapper<TestClassA, TestClassB>.GetTypeMapping("thisConfigDoesNotExist");
            Assert.IsNull(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTypeMappingNullSectionTest()
        {
            ConfigurationMapper<TestClassA, TestClassB>.GetTypeMapping(null);
        }

        [TestMethod()]
        public void GetTypeMappingTest()
        {
            var actual = ConfigurationMapper<TestClassA, TestClassB>.GetTypeMapping();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void MapTest()
        {
            ConfigurationMapper<TestClassA, TestClassB> target = new ConfigurationMapper<TestClassA, TestClassB>();
            TestClassA from = new TestClassA() { Name = "test" };
            TestClassB to = new TestClassB();
            target.Map(from, to);
            Assert.AreEqual(from.Name, to.InnerClassName);
        }

        #endregion Methods
    }
}