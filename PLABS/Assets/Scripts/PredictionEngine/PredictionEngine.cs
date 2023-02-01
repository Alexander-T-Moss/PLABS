using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PredictionEngine : MonoBehaviour
{
    // Prediction scene components
    private Scene _predictionScene;
    private PhysicsScene _predictionPhysicsScene;

    // Lists of GameObjects PredictionEngine manages
    public List<GameObject> RealGameObjects;
    public List<GameObject> GhostGameObjects;
    public List<GameObject> TrajectoryLines;

    private PhysicsEngineController _physicsEngineController;

    // Runs when GameObject this script is attached to is made active
    private void Awake()
    {
        // Create predictionScene and predicitonPhysicsScene
        CreateSceneParameters sceneParameters = new(LocalPhysicsMode.Physics3D);
        _predictionScene = SceneManager.CreateScene("predictionScene", sceneParameters);
        _predictionPhysicsScene = _predictionScene.GetPhysicsScene();
    }

    // Runs when GameObject this script is attached to is enabled
    private void OnEnable()
    {
        _physicsEngineController = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngineController>();
        //PredictTrajectories(5);
        //ClearTrajectoryPredictions();
    }
    
    // Runs every frame update
    private void Update()
    {
        // Run PredictTrajectories() when Return key is pressed
        if(Input.GetKeyDown(KeyCode.Return))
            PredictTrajectories();
    }

    // Loads all gameObjects with tag PhysicsBody into predictionScene
    private void LoadGhostObjects()
    {
        // Loads all game objects with tag "PhysicsBody"
        RealGameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("PhysicsBody"));

        // Remove old trajectory lines
        ClearTrajectoryPredictions();

        // Add clones and new trajectory lines
        for(int indexer = 0; indexer < RealGameObjects.Count; indexer++)
        {
            GhostGameObjects.Add(CreateClone(RealGameObjects[indexer], "predictionScene"));

            // Only create trajectory lines for game objects that are no kinematic
            if(!RealGameObjects[indexer].GetComponent<Rigidbody>().isKinematic)
                TrajectoryLines.Add(CreateTrajectoryLine(RealGameObjects[indexer].name + "'s Trajectory Line"));
            else
                TrajectoryLines.Add(null);
        }
    }

    // Removes all GameObjects in predictionScene
    private void UnloadGhostObjects()
    {
        for(int indexer = 0; indexer < GhostGameObjects.Count; indexer++)
        {
            Destroy(GhostGameObjects[indexer]);
        }
        GhostGameObjects = new();
        RealGameObjects = new();
    }

    // Creates clone of a GameObject and spawns it in a scene
    private GameObject CreateClone(GameObject _gameObjectToBeCloned, string _cloneSpawnScene)
    {
        // Create _clonedGameObject GameObject
        GameObject _clonedGameObject;
        SceneManager.MoveGameObjectToScene(_clonedGameObject = Instantiate(_gameObjectToBeCloned), SceneManager.GetSceneByName(_cloneSpawnScene));

        // Modify _clonedGameObject's GameObject parameters
        _clonedGameObject.tag = "Clone";
        _clonedGameObject.GetComponent<MeshRenderer>().enabled = false;
        _clonedGameObject.GetComponent<Rigidbody>().velocity = _gameObjectToBeCloned.GetComponent<Rigidbody>().velocity;
        _clonedGameObject.GetComponent<PhysicsParameters>().UpdateParameters();

        return _clonedGameObject;
    }

    // Creates trajectory line GameObject
    private GameObject CreateTrajectoryLine(string _objectName)
    {
        // Create _trajectoryLine GameObject
        GameObject _trajectoryLine = new(_objectName);
        _trajectoryLine.AddComponent<LineRenderer>();

            
        // Modify _trajectoryLine's GameObject parameters
        LineRenderer _trajectoryLineRenderer = _trajectoryLine.GetComponent<LineRenderer>();
        _trajectoryLineRenderer.startWidth = 0.05f;
        _trajectoryLineRenderer.endWidth = 0.05f;

        return _trajectoryLine;
    }


    private void PredictTrajectories(int _steps = 1000)
    {
        LoadGhostObjects();

        for(int indexer = 0; indexer < TrajectoryLines.Count; indexer++)
        {
            if (TrajectoryLines[indexer] != null)
                TrajectoryLines[indexer].GetComponent<LineRenderer>().positionCount = _steps;
        }

        for(int _step = 0; _step < _steps; _step++)
        {
            _physicsEngineController.PhysicsEngineUpdate("Clone");
            _predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
        
            for(int indexer = 0; indexer < GhostGameObjects.Count; indexer++)
            {
                if (!GhostGameObjects[indexer].GetComponent<Rigidbody>().isKinematic)
                    TrajectoryLines[indexer].GetComponent<LineRenderer>().SetPosition(_step, GhostGameObjects[indexer].GetComponent<Rigidbody>().position);
            }
        }

        UnloadGhostObjects();
    }


    private void ClearTrajectoryPredictions()
    {
        for(int indexer = 0; indexer < TrajectoryLines.Count; indexer++)
        {
            Destroy(TrajectoryLines[indexer]);
        }
        TrajectoryLines = new();
    }
}