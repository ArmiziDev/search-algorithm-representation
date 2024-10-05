using Godot;
using System;
using System.Collections.Generic;

public partial class State : Node2D
{
    private List<bool> visited_targets = new List<bool>();
    private StringName Atomic; // atomic representation of state

    [Export] private Sprite2D Sprite;

    public State(StringName Name, List<bool> visited_targets)
    {
        Initialize(Name, visited_targets);
    }

    public State()
    {

    }

    public void Initialize(StringName Name, List<bool> visited_targets)
    {
        setName(Name);
        setVisitedTargets(visited_targets);

        Atomic = Name + "-";
        foreach (bool visited in visited_targets)
        {
            if (visited)
            {
                Atomic += "1";
            }
            else
            {
                Atomic += "0";
            }
        }
    }
    private void setName(StringName Name)
    {
        this.Name = Name;
    }
    private void setPosition(Vector2 Position)
    {
        this.Position = Position;
    }
    private void setVisitedTargets(List<bool> visited_targets) 
    {
        this.visited_targets = visited_targets;
    }

    public StringName getName() { return this.Name; }
    public List<bool> getVisitedTargets() { return this.visited_targets; }
    public StringName getRepresentation() { return this.Atomic; }

    public void Glow()
    {
        if (Sprite == null) return;

        Sprite.Modulate = new Color(1, 1, 0);
    }

    public void RemoveGlow()
    {
        if (Sprite == null) return;

        Sprite.Modulate = new Color(1,1,1);
    }
}
