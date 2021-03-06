﻿using System;
using System.Runtime.Serialization;

namespace Alex.GuiDebugger.Common
{

	[DataContract]
	public class GuiElementPropertyInfo
	{
		
		[DataMember]
		public virtual string Name { get; set; }

		[DataMember]
		public virtual Type Type { get; set; }

		[DataMember]
		public virtual object Value { get; set; }

		[DataMember]
		public virtual string StringValue { get; set; }
	}
}
