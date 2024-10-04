using Godot;
using System;
using System.Collections.Generic;

public partial class SearchRequest : Node
{
	private StringName Start;
	private List<StringName> Targets = new List<StringName>();

	public void Initialize(StringName Name, StringName Start, List<StringName> Targets)
	{
		this.Name = Name;
		this.Start = Start;
		this.Targets = Targets;
	}

	public StringName getName() { return this.Name; }
	public StringName getStartLocation() { return this.Start; }
	public List<StringName> getTargets() { return this.Targets; }
}
