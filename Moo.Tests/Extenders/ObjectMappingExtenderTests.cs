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
using FakeItEasy;
using Moo.Extenders;
using NUnit.Framework;

namespace Moo.Tests.Extenders
{
    [TestFixture]
    public class ObjectMappingExtenderTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullMapper_Throws_1()
        {
            var source = new TestClassE();
            source.MapTo<TestClassB>((IMapper) null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullMapper_Throws_2()
        {
            var source = new TestClassE();
            source.MapTo(new TestClassB(), (IMapper) null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullRepo_Throws_1()
        {
            var source = new TestClassD();
            source.MapTo(new TestClassA(), (IMappingRepository) null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullRepo_Throws_2()
        {
            var source = new TestClassD();
            source.MapTo<TestClassA>((IMappingRepository) null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullSource_Throws_1()
        {
            TestClassA source = null;
            source.MapTo(new TestClassB());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullSource_Throws_2()
        {
            TestClassA source = null;
            source.MapTo<TestClassB>();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullSource_Throws_3()
        {
            TestClassA source = null;
            source.MapTo<TestClassB>(A.Fake<IMapper>());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullSource_Throws_4()
        {
            TestClassA source = null;
            source.MapTo<TestClassB>(A.Fake<IMappingRepository>());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullSource_Throws_5()
        {
            TestClassA source = null;
            source.MapTo(new TestClassB(), A.Fake<IMapper>());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullTarget_Throws_1()
        {
            var source = new TestClassB();
            source.MapTo((TestClassC) null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullTarget_Throws_2()
        {
            var source = new TestClassB();
            source.MapTo((TestClassC) null, A.Fake<IMapper>());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MapTo_NullTarget_Throws_3()
        {
            var source = new TestClassB();
            source.MapTo((TestClassC) null, A.Fake<IMappingRepository>());
        }

        [Test]
        public void MapTo_ValidInputAndTarget_UsesDefaultRepo()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map(source, (object) expected)).Returns(expected);
            MappingRepository.Default.Clear();
            MappingRepository.Default.AddMapper(mapperMock);
            TestClassB actual = source.MapTo(expected);
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }

        [Test]
        public void MapTo_ValidInputAndTarget_UsesProvidedMapper()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map(source, (object) expected)).Returns(expected);
            TestClassB actual = source.MapTo(expected, mapperMock);
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }

        [Test]
        public void MapTo_ValidInputAndTarget_UsesProvidedRepo()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map(source, (object) expected)).Returns(expected);
            var repoMock = A.Fake<IMappingRepository>();
            A.CallTo(() => repoMock.ResolveMapper(typeof (TestClassC), typeof (TestClassB))).Returns(mapperMock);
            TestClassB actual = source.MapTo(expected, repoMock);
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }

        [Test]
        public void MapTo_ValidInput_UsesDefaultRepo()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map((object) source)).Returns(expected);
            MappingRepository.Default.Clear();
            MappingRepository.Default.AddMapper(mapperMock);
            var actual = source.MapTo<TestClassB>();
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }

        [Test]
        public void MapTo_ValidInput_UsesProvidedMapper()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map((object) source)).Returns(expected);
            var actual = source.MapTo<TestClassB>(mapperMock);
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }

        [Test]
        public void MapTo_ValidInput_UsesProvidedRepo()
        {
            var source = new TestClassC();
            var mapperMock = A.Fake<IExtensibleMapper<TestClassC, TestClassB>>();
            var expected = new TestClassB();
            A.CallTo(() => mapperMock.Map((object) source)).Returns(expected);
            var repoMock = A.Fake<IMappingRepository>();
            A.CallTo(() => repoMock.ResolveMapper(typeof (TestClassC), typeof (TestClassB))).Returns(mapperMock);
            var actual = source.MapTo<TestClassB>(repoMock);
            Assert.AreEqual(expected, actual);
            MappingRepository.Default.Clear();
        }
    }
}