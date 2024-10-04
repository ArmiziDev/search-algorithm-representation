using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SearchAlgorithms : Node
{
	public SearchResults breadthFirstSearch(Problem problem)
	{
		StringName initial_state_name = problem.getInitialState();
		List<bool> initial_targets = new List<bool>();
		State initial_state = new State(initial_state_name, initial_targets);

		List<State> pending_nodes = new List<State>();
		pending_nodes.Add(initial_state);
		int nodes_reached = 1;
		int nodes_explored = 0;

		Dictionary<StringName, State> explore_map = new Dictionary<StringName, State>();

		while (pending_nodes.Count > 0)
		{
			State current_state = pending_nodes[0];
			pending_nodes.Remove(current_state);
			nodes_explored += 1;

			if (problem.isGoalState(current_state))
			{
				float cost = 0;
				List<StringName> solution_path = new List<StringName>();

				while(current_state != null)
				{
					solution_path.Add(current_state.Name);
					State previous_state = current_state;
					current_state = explore_map[current_state.getRepresentation()];
					if (current_state != null)
					{
						cost += problem.GetCityMap().getCost(previous_state.Name, current_state.Name);
					}
				}

				return new SearchResults(solution_path, cost, nodes_reached, nodes_explored);
			}

			List<State> children = problem.generateChildren(current_state);

			foreach (State child in children)
			{
				StringName child_representation = child.getRepresentation();

				if (!explore_map.ContainsKey(child_representation))
				{
					pending_nodes.Append<State>(child);
					explore_map[child_representation] = current_state;
					nodes_reached++;
				}
			}
		}

		return new SearchResults(null, 0, 0, 0);
    }
}
