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

namespace Moo.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moo.Core;

    /// <summary>
    /// Maps between two classes by using the mapping attributes in their members.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    public class AttributeMapper<TSource, TTarget> : BaseMapper<TSource, TTarget>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeMapper&lt;TSource, TTarget&gt;"/> class.
        /// </summary>
        public AttributeMapper()
            : base()
        {
            this.GenerateMappings();
        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (1) 

        /// <summary>
        /// Generates the member mappings and adds them targetType the provided <see cref="TypeMappingInfo{TSource, TTarget}"/> object.
        /// </summary>
        /// <param name="typeMapping">The type mapping where discovered mappings will be added.</param>
        protected override void GenerateMappings(TypeMappingInfo<TSource, TTarget> typeMapping)
        {
            var fromType = typeof(TSource);
            var toType = typeof(TTarget);

            AddMappings(fromType, toType, MappingDirections.From, typeMapping);
            AddMappings(toType, fromType, MappingDirections.To, typeMapping);
        }

        // Private Methods (1) 

        /// <summary>
        /// Adds the mappings based on the existing mapping attributes.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="direction">The mapping direction.</param>
        /// <param name="typeMapping">The object where mappings will be added.</param>
        private static void AddMappings(
            Type sourceType,
            Type targetType,
            MappingDirections direction,
            TypeMappingInfo<TSource, TTarget> typeMapping)
        {
            var q = from prop in sourceType.GetProperties()
                    from MappingAttribute m in prop.GetCustomAttributes(typeof(MappingAttribute), false)
                    where (m.Direction & direction) == direction
                    where m.OtherType.IsAssignableFrom(targetType)
                    select new KeyValuePair<PropertyInfo, MappingAttribute>(prop, m);

            foreach (var kvp in q)
            {
                ReflectionPropertyMappingInfo<TSource, TTarget> mappingInfo = null;
                if (direction == MappingDirections.From)
                {
                    mappingInfo = new ReflectionPropertyMappingInfo<TSource, TTarget>(
                        kvp.Key,
                        targetType.GetProperty(kvp.Value.OtherMemberName),
                        true);
                }
                else
                {
                    mappingInfo = new ReflectionPropertyMappingInfo<TSource, TTarget>(
                        targetType.GetProperty(kvp.Value.OtherMemberName),
                        kvp.Key,
                        true);
                }

                typeMapping.Add(mappingInfo);
            }
        }

        #endregion Methods
    }
}