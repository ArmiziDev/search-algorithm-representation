using Godot;
using System;
using System.Collections.Generic;
using System.Transactions;
using static Godot.WebSocketPeer;

public partial class DrawMap : Node2D
{
    [Export] PackedScene StateScene;

    State current_state;
    List<State> visual_states = new List<State>();

    public void drawMap(CityMap _map)
    {
        foreach (StringName state_name in _map.getCityList())
        {
            State current_state = StateScene.Instantiate<State>();
            visual_states.Add(current_state);
            current_state.Position = _map.getMapPositions()[state_name];
            current_state.Name = state_name;
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
    public State onState(State onstate)
    {
        current_state = visual_states.Find(state => state.Name == onstate.Name);
        return current_state;
    }
    public void replaceState(State replace_state)
    {
        State removing_state = visual_states.Find(state => state.Name == replace_state.Name);
        int index = visual_states.FindIndex(state => state.Name == replace_state.Name);

        replace_state.Position = removing_state.Position;

        if (index != -1)
        {
            RemoveChild(removing_state);
            AddChild(replace_state);
            visual_states[index] = replace_state;
        }
    }
}
