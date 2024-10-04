using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Problem : Node
{
	private CityMap Map { get; set; }
	private SearchRequest currentCase { get; set; }
	public void Initialize(CityMap map, SearchRequest request)
	{
		Map = map;
        currentCase = request;
	}

	public CityMap GetCityMap() { return Map; }
	public SearchRequest GetRequest() { return currentCase; }
	public StringName getInitialState()
	{
		return currentCase.getStartLocation();
    }
	public bool isGoalState(State state)
	{
		if (state.Name != currentCase.getName()) return false;
		if (state.getVisitedTargets().Contains(false)) return false;
		return true;
	}
	public List<State> generateChildren(State state)
	{
		StringName original_loc = state.Name;
		List<StringName> children_list_names = GetCityMap().getNeighbors(original_loc);

		List<State> children = new List<State>();
		foreach (StringName state_name in children_list_names)
		{
			List<bool> visited_targets = state.getVisitedTargets();

			if (GetRequest().getTargets().Contains(state_name))
			{
				int target_index = GetRequest().getTargets().IndexOf(state_name);
                visited_targets[target_index] = true;
			}
			State new_state = new State(state_name, visited_targets);
			children.Add(new_state);
		}
		return children;
	}

	public float getActionState(State state, StringName action)
	{
		StringName state_name = state.getName();
		return GetCityMap().getCost(state_name, action);
	}
}
