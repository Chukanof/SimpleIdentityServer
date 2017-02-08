﻿#region copyright
// Copyright 2017 Habart Thierry
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SimpleIdentityServer.EventStore.EF.Parsers
{
    public class WhereInstruction : BaseInstruction
    {
        public const string Name = "where";

        public WhereInstruction()
        {
        }

        public override KeyValuePair<string, Expression>? GetExpression(Type sourceType, ParameterExpression rootParameter)
        {
            var splitted = Regex.Split(Regex.Replace(Parameter, @"\s+", ""), "eq");
            if (splitted.Count() != 2)
            {
                throw new ArgumentException("the filter is not correct");
            }

            var sourceQueryableType = typeof(IQueryable<>).MakeGenericType(sourceType);
            var propertyName = splitted.First();
            var value = splitted.ElementAt(1);
            var propertyInfo = sourceType.GetProperty(propertyName);
            var arg = Expression.Parameter(sourceType, "x");
            // var fArg = Expression.Parameter(sourceQueryableType, "e");
            var property = Expression.Property(arg, propertyName);
            var equalExpr = Expression.Equal(property, Expression.Constant(value));
            var selector = Expression.Lambda(equalExpr, new ParameterExpression[] { arg });
            var enumarableType = typeof(Queryable);
            var genericMethod = enumarableType.GetMethods()
                 .Where(m => m.Name == "Where" && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     return parameters.Count == 2;
                 }).First().MakeGenericMethod(sourceType);
            if (SubInstruction != null)
            {
                var subExpr = SubInstruction.GetExpression(sourceType, rootParameter);
                var call = Expression.Call(genericMethod, subExpr.Value.Value, selector);
                return new KeyValuePair<string, Expression>(Name, call);
            }

            return new KeyValuePair<string, Expression>(Name, Expression.Call(genericMethod, rootParameter, selector));
        }
    }
}