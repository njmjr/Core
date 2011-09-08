﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.f
// See the License for the specific language governing permissions and
// limitations under the License.

#if !SILVERLIGHT
namespace Castle.Components.DictionaryAdapter.Xml
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class XmlArraySerializer : XmlCollectionSerializer
	{
		public static readonly XmlArraySerializer
			Instance = new XmlArraySerializer();

		protected XmlArraySerializer() { }

		public override bool CanGetStub { get { return true; } }

		public override object GetStub(IXmlCursor cursor, IDictionaryAdapter parent, IXmlAccessor accessor)
		{
			var itemType = accessor.ClrType.GetElementType();

			return Array.CreateInstance(itemType, 0);
		}

		public override object GetValue(IXmlNode node, IDictionaryAdapter parent, IXmlAccessor accessor)
		{
			var items    = new ArrayList();
			var itemType = accessor.ClrType.GetElementType();

			accessor.GetCollectionAccessor(itemType)
				.GetCollectionItems(node, parent, items);

			return items.ToArray(itemType);
		}

		public override void SetValue(IXmlNode node, IXmlAccessor accessor, object value)
		{
			var itemType = value.GetType().GetElementType();

			accessor.GetCollectionAccessor(itemType)
				.SetCollectionItems(node, (IEnumerable) value);
		}
	}
}
#endif