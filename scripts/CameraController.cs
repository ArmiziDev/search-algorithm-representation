using Godot;

public partial class CameraController : Node2D
{
    private bool isDragging = false; // To track if the mouse button is held down
    private Vector2 previousMousePosition;
    private const float zoomSpeed = 0.1f; // The speed at which the camera zooms in and out
    private const float minZoom = 0.5f; // Minimum zoom level
    private const float maxZoom = 2.0f; // Maximum zoom level

    [Export] Camera2D camera;

    public override void _Input(InputEvent @event)
    {
        // Check for left mouse button press
        if (@event is InputEventMouseButton mouseEvent)
        {
            // If the left mouse button is pressed
            if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
            {
                isDragging = true;
                previousMousePosition = GetGlobalMousePosition(); // Get initial mouse position
            }
            // If the left mouse button is released
            else if (mouseEvent.ButtonIndex == MouseButton.Left && !mouseEvent.Pressed)
            {
                isDragging = false;
            }

            // Handle zooming with the scroll wheel
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp && mouseEvent.Pressed)
            {
                ZoomOut();
            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown && mouseEvent.Pressed)
            {
                ZoomIn();
            }
        }

        // Check for mouse motion while dragging
        if (@event is InputEventMouseMotion mouseMotionEvent && isDragging)
        {
            Vector2 currentMousePosition = GetGlobalMousePosition();
            Vector2 delta = previousMousePosition - currentMousePosition;

            // Move the camera by the delta
            Position += delta;

            previousMousePosition = currentMousePosition; // Update the previous mouse position
        }
    }

    // Method to zoom in
    private void ZoomIn()
    {
        Vector2 newZoom = camera.Zoom - new Vector2(zoomSpeed, zoomSpeed); // Decrease zoom (zoom in)
        newZoom = newZoom.Clamp(new Vector2(minZoom, minZoom), new Vector2(maxZoom, maxZoom)); // Clamp zoom to allowed limits
        camera.Zoom = newZoom;
    }

    // Method to zoom out
    private void ZoomOut()
    {
        Vector2 newZoom = camera.Zoom + new Vector2(zoomSpeed, zoomSpeed); // Increase zoom (zoom out)
        newZoom = newZoom.Clamp(new Vector2(minZoom, minZoom), new Vector2(maxZoom, maxZoom)); // Clamp zoom to allowed limits
        camera.Zoom = newZoom;
    }
}
