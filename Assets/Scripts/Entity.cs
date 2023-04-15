using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Components
    private Rigidbody rigidbody;

    // Movement variables
    private Vector3 direction;
    private float currentSpeed;

    // Ability variables
    private Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();

    // Obstacle avoidance variables
    private List<Obstacle> obstacles = new List<Obstacle>();

    void Start()
    {
        // Get the Rigidbody component
        rigidbody = GetComponent<Rigidbody>();

        // Initialize abilities
        abilities.Add("Turn", new TurnAbility());
        abilities.Add("Accelerate", new AccelerateAbility());
        abilities.Add("Brake", new BrakeAbility());
    }

    void Update()
    {
        // Update the direction of movement based on user input or AI
        // ...

        // Check for obstacles and update the obstacles list
        obstacles = ObstacleDetection();

        // Use the brain to select the appropriate ability based on the obstacles
        Ability selectedAbility = Brain.SelectAbility(abilities, obstacles);

        // Use the selected ability to update the direction and speed of movement
        selectedAbility.UpdateMovement(this);

        // Move in the current direction and speed
        rigidbody.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    // Detect obstacles using raycasting
    private List<Obstacle> ObstacleDetection()
    {
        List<Obstacle> obstacles = new List<Obstacle>();
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction);

        foreach (RaycastHit hit in hits)
        {
            Obstacle obstacle = new Obstacle();
            obstacle.distance = hit.distance;
            obstacle.normal = hit.normal;
            obstacle.collider = hit.collider;
            obstacles.Add(obstacle);
        }

        return obstacles;
    }

    // Set the current direction and speed of movement
    public void SetMovement(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.currentSpeed = speed;
    }
}

public struct EntityData
{
    public Vector3 position;
    public Vector3 velocity;
    public Quaternion rotation;
    // add any other fields here that you need
}
