using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject PuzzlePiece;

    //private Vector3 position;
    //private float width;
    //private float height;

    //void Awake()
    //{
    //    width = (float)Screen.width / 2.0f;
    //    height = (float)Screen.height / 2.0f;

    //    // Position used for the cube.
    //    position = new Vector3(0.0f, 0.0f, 0.0f);
    //}

    //void OnGUI()
    //{
    //    // Compute a fontSize based on the size of the screen width.
    //    GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

    //    GUI.Label(new Rect(20, 20, width, height * 0.25f),
    //        "x = " + position.x.ToString("f2") +
    //        ", y = " + position.y.ToString("f2"));
    //}

    private void OnMouseDown()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            // Convert the touch position into a ray
            Ray ray = Camera.main.ScreenPointToRay(hit.point);

            // If we hit something...
            GameObject touchedObject = hit.transform.gameObject;

            if (touchedObject == gameObject)
            {
                if (PuzzlePiece != null)
                {
                    PuzzlePiece.SetActive(true);
                    var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
                    puzzleManager.ValidatePuzzle();
                }
            }
        }
    }

    void Update()
    {
        //// Check if there's a touch
        //if (Input.touchCount > 0)
        //{
        //    // Get the first touch (you might loop through all touches if you want multi-touch)
        //    Touch touch = Input.GetTouch(0);

        //    // Convert the touch position into a ray
        //    Ray ray = Camera.main.ScreenPointToRay(touch.position);

        //    // This will store information about the raycast
        //    RaycastHit hit;

        //    // Perform the raycast
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        // If we hit something...
        //        GameObject touchedObject = hit.transform.gameObject;

        //        if (touchedObject == gameObject)
        //        {
        //            if (PuzzlePiece != null )
        //            {
        //                PuzzlePiece.SetActive(true);
        //                var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        //                puzzleManager.ValidatePuzzle();
        //            }
        //        }
        //    }
        //}
    }
}
