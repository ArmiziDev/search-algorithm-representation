using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;

public partial class SearchResults : Node
{
    List<StringName> solutionPath;
    float solutionCost;
    int nodesReached;
    int nodesExplored;

    public SearchResults(List<StringName> solutionPath, float solutionCost, int nodesReached, int nodesExplored)
    {
        Initialize(solutionPath, solutionCost, nodesReached, nodesExplored);  
    }

    public void Initialize(List<StringName> solution_path, float solution_cost, int nodes_reached, int nodes_explored)
	{
        solutionCost = solution_cost;
        solutionPath = solution_path;
        nodesReached = nodes_reached;
        nodesExplored = nodes_explored;
    }

    public void PrintSolution()
    {
        GD.Print("Current Solution Cost " + solutionCost);
        GD.Print("Current Solution Path");
        String path = "";
        foreach (StringName name in solutionPath)
        {
            path += name + " -> ";
        }
        GD.Print(path);
        GD.Print("Nodes Reached: " + nodesReached);
        GD.Print("Nodes Explroed: " + nodesExplored);
    }
}
