using Pathfinding;
using Pathfinding.AIComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Util;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Events;

namespace RPG.InteractiveStateManagers
{
    /// <summary>Helper script in the example scene 'Turn Based'</summary>
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_manager.php")]
    public class TurnBasedPathfinder : MonoBehaviour
    {//each unit could reference the scriptable object directly, where they could add themselves to the queue and then update it from this class
        
        public LayerMask layerMask;
        public float movementSpeed;//how quickly a unit goes from A to B
        protected TurnBasedAI selected;
        protected List<GameObject> possibleMoves = new List<GameObject>();
        protected EventSystem eventSystem;

        protected List<GameObject> targettedObjects;

        public delegate void OnStateChanged(TurnState state);

        public event OnStateChanged onStateChanged;
        public GameObject moveNodePrefab;
        public GameObject attackNodePrefab;


        TurnState state = TurnState.SelectUnit;
        public TurnState State
        {
            get => state;
            set
            {
                state = value;
                onStateChanged.Invoke(state);
            }
        }

        private void OnEnable()
        {
            onStateChanged += UpdateState;
        }

        private void OnDisable()
        {

           onStateChanged -= UpdateState;
        }
        protected void Awake()
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
        void Update()
        {
            CheckMouse();
        }

        private void UpdateState(TurnState state)
        {
            this.state = state;
        }

        private void CheckMouse()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //// Ignore any input while the mouse is over a UI element
            //if (eventSystem.IsPointerOverGameObject()) 
            //{
            //	Debug.Log("Over UI"); always returning true
            //	return;
            //}

            if (state == TurnState.SelectTarget)
            {
                HandleButtonUnderRay(ray);
            }

            if (state == TurnState.SelectUnit || state == TurnState.SelectTarget)
            {
                //if (Input.GetKeyDown(KeyCode.Mouse0))
                //{
                //	var unitUnderMouse = GetByRay<TurnBasedAI>(ray);
                //	CheckMovement(unitUnderMouse);
                //}
            }
        }

        // TODO: Move to separate class
       

        void HandleButtonUnderRay(Ray ray)
        {
            var button = GetByRay<AStar2DButton>(ray);

            if (button != null && Input.GetKeyDown(KeyCode.Mouse0))
            {
                button.OnClick();

                DestroyPossibleMoves();
                state = TurnState.Move;
                StartCoroutine(MoveToNode(selected, button.node));

            }
        }
        IEnumerator MoveToNode(TurnBasedAI unit, GraphNode node)
        {
            var path = ABPath.Construct(unit.transform.position, (Vector3)node.position);

            path.traversalProvider = unit.traversalProvider;

            // Schedule the path for calculation
            AstarPath.StartPath(path);

            // Wait for the path calculation to complete
            yield return StartCoroutine(path.WaitForPath());

            if (path.error)
            {
                // Not obvious what to do here, but show the possible moves again
                // and let the player choose another target node
                // Likely a node was blocked between the possible moves being
                // generated and the player choosing which node to move to
                Debug.LogError("Path failed:\n" + path.errorLog);
               state = TurnState.SelectTarget;
                GeneratePossibleMoves(selected);
                yield break;
            }

            // Set the target node so other scripts know which
            // node is the end point in the path
            unit.targetNode = path.path[path.path.Count - 1];

            yield return StartCoroutine(MoveAlongPath(unit, path, movementSpeed));

            unit.blocker.BlockAtCurrentPosition();
            // Select a new unit to move
            state = TurnState.SelectUnit;
        }


        public void CheckMovement(TurnBasedAI aiSelected)
        {
            if (aiSelected != null)
            {
                Select(aiSelected);
                DestroyPossibleMoves();
                GeneratePossibleMoves(selected);

                state = TurnState.SelectTarget;
            }
        }
        static IEnumerator MoveAlongPath(TurnBasedAI unit, ABPath path, float speed)
        {
            if (path.error || path.vectorPath.Count == 0)
                throw new System.ArgumentException("Cannot follow an empty path");

            // Very simple movement, just interpolate using a catmull rom spline
            float distanceAlongSegment = 0;
            for (int i = 0; i < path.vectorPath.Count - 1; i++)
            {
                var p0 = path.vectorPath[Mathf.Max(i - 1, 0)];
                // Start of current segment
                var p1 = path.vectorPath[i];
                // End of current segment
                var p2 = path.vectorPath[i + 1];
                var p3 = path.vectorPath[Mathf.Min(i + 2, path.vectorPath.Count - 1)];

                var segmentLength = Vector3.Distance(p1, p2);

                while (distanceAlongSegment < segmentLength)
                {
                    var interpolatedPoint = AstarSplines.CatmullRom(p0, p1, p2, p3, distanceAlongSegment / segmentLength);
                    unit.transform.position = interpolatedPoint;
                    yield return null;
                    distanceAlongSegment += Time.deltaTime * speed;
                }

                distanceAlongSegment -= segmentLength;
            }

            unit.transform.position = path.vectorPath[path.vectorPath.Count - 1];
        }
        protected void Select(TurnBasedAI unit)
        {
            selected = unit;
        }
        protected void DestroyPossibleMoves()
        {
            foreach (var go in possibleMoves)
            {
                Destroy(go);
            }
            possibleMoves.Clear();
        }
        protected void GeneratePossibleMoves(TurnBasedAI unit)
        {
            var path = ConstantPath.Construct(unit.transform.position, unit.movementPoints * 1000 + 1);

            path.traversalProvider = unit.traversalProvider;

            // Schedule the path for calculation
            AstarPath.StartPath(path);

            // Force the path request to complete immediately
            // This assumes the graph is small enough that
            // this will not cause any lag
            path.BlockUntilCalculated();

            foreach (var node in path.allNodes)
            {
                if (node != path.startNode)
                {
                    // Create a new node prefab to indicate a node that can be reached
                    // NOTE: If you are going to use this in a real game, you might want to
                    // use an object pool to avoid instantiating new GameObjects all the time
                    var go = Instantiate(moveNodePrefab, (Vector3)node.position, Quaternion.identity) as GameObject;
                    possibleMoves.Add(go);

                    go.GetComponent<AStar2DButton>().node = node;
                }
            }
        }
       
        public T GetByRay<T>(Ray ray) where T : class
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;

            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero, float.PositiveInfinity, layerMask);

            if (hit)
            {
                return hit.transform.GetComponentInParent<T>();
            }
            return null;
        }
        /// <summary>Interpolates the unit along the path</summary>
    }
    public enum TurnState
    { 
        //enables and disables certain actions during each TurnState
        SelectUnit, //Default, used when preparing an action
        SelectTarget, // Used when an action with a target has been selected (Moving, skills, attacking)
        Move, // used during moving
        Attack, // used during attacking/Skills
    }
}

