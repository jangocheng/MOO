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

namespace Moo.Tests.Integration
{
    using Moo.Tests.Integration.MappedClasses.DomainModels;
    using Moo.Tests.Integration.MappedClasses.ViewModels;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Shouldly;

    [TestFixture]
    public class SimpleMappingTest
    {
        [Test]
        public void Map_SimpleCase_MapsCorrectly()
        {
            var source = CreateSource();
            var mapper = MappingRepository.Default.ResolveMapper<Person, PersonEditModel>();

            var result = mapper.Map(source);

            result.ShouldNotBe(null);
            result.Id.ShouldBe(source.Id);
        }

        [Test]
        public void ExtensionMap_SimpleCase_MapsCorrectly()
        {
            var source = CreateSource();

            var result = source.MapTo<PersonEditModel>();

            CheckMapping(source, result);
        }

        [Test]
        public void ExtensionMapMultiple_SimpleCase_MapsCorrectly()
        {
            var source = CreateMany();

            var result = source.MapAll<Person, PersonEditModel>();

            result.ShouldNotBe(null);
        }

        private void CheckMapping(Person p, PersonEditModel pe)
        {
            pe.ShouldNotBe(null);
            pe.Id.ShouldBe(p.Id);
        }

        private Person CreateSource()
        {
            return new Person()
            {
                Id = 1234,
                LastName = "Doe",
                FirstName = "John"
            };
        }

        private IEnumerable<Person> CreateMany()
        {
            return Enumerable.Range(0, 5).Select(i => CreateSource());
        }
    }
}
