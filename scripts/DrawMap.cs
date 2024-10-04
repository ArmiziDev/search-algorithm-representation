using Godot;
using System;
using System.Collections.Generic;

public partial class DrawMap : Node2D
{

    [Export] PackedScene StateScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public void drawMap(CityMap _map)
    {
        foreach (StringName state_name in _map.getCityList())
        {
            State current_state = StateScene.Instantiate<State>();
            current_state.Position = _map.getMapPositions()[state_name];
            List<StringName> neighbors = _map.getNeighbors(state_name);

            Line2D line = new Line2D();
            line.DefaultColor = new Color(1, 1, 1);
            line.Width = 2;

            line.AddPoint(current_state.Position);

            foreach (StringName neighbor in neighbors)
            {
                Vector2 neighborPosition = _map.getMapPositions()[neighbor];
                line.AddPoint(neighborPosition);
            }

            AddChild(current_state);
            AddChild(line);
        }
    }
}
