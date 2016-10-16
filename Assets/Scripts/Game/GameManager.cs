using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public enum GameState
    {
        START, PICKING, INSPECTING, END
    }
    private GameState gameState = GameState.START;

    private GameObject[] inspectionButtons;
    private MushroomScript currentMushroom;
    private int totalPicked = 0;
    private int correctPicked = 0;
    private MushroomScript.MushroomName desiredMushroom = MushroomScript.MushroomName.BUTTON;
    private HungerBar hungerBar;
    private Text mushroomCountText;
    private CurrentLevelInfo levelInfo;
    private OnScreenText screenMessage;
    public Vector2 mousePositionStart;
    public Vector2 mousePositionHold;
    private GameObject hitObject = null;
    public float swipeThreshold = 5f;
    public GameObject trippingEffect;
    private MainCamera mainCamera;
    private int grassPlucked = 0;
    private int leavesBrushed = 0;


    public GameState GetGameState()
    {
        return gameState;
    }

    public void StartPicking()
    {
        StartCoroutine(PlayMessageThenChangeState("Start Picking!", GameState.PICKING));
        
    }

    public IEnumerator PlayMessageThenChangeState(string message, GameState state)
    {
        screenMessage.DisplayMessage(message);
        yield return new WaitForSeconds(2f);
        gameState = state;
    }

    public void TimeOver()
    {
        gameState = GameState.END;
        SetLevelInfo();
        SceneManager.LoadScene("LevelComplete");
    }

    private void SetLevelInfo()
    {
        levelInfo.leavesBrushed = leavesBrushed;
        levelInfo.grassPicked = grassPlucked;
    }

    public void HungerDepleted()
    {
        gameState = GameState.END;
        SetLevelInfo();
        SceneManager.LoadScene("LevelComplete");
    }

    public void KeepIt()
    {
        HideInspectionOptions();
        totalPicked++;
        mushroomCountText.text = "Mushrooms Picked\n" + totalPicked.ToString();
        levelInfo.AddMushroom(currentMushroom.mushroomName);
        if (currentMushroom.mushroomName.Equals(desiredMushroom))
        {
            correctPicked++;
        }
        gameState = GameState.PICKING;
        currentMushroom.KeepItAnimation();
        
    }

    public void TossIt()
    {
        HideInspectionOptions();
        gameState = GameState.PICKING;
        currentMushroom.TossItAnimation();
        
    }

    public void EatIt()
    {
        if(currentMushroom.mushroomType == MushroomScript.MushroomType.EDIBLE)
        {
            hungerBar.EatEdibleShroom();
        }else if(currentMushroom.mushroomType == MushroomScript.MushroomType.POISON)
        {
            hungerBar.EatPoisonousShroom();
            mainCamera.StopTripping();
            
        }else if(currentMushroom.mushroomType == MushroomScript.MushroomType.TRIPPY)
        {
            hungerBar.StartTripping();
            mainCamera.StartTripping();
        }
        HideInspectionOptions();
        gameState = GameState.PICKING;
        currentMushroom.EatItAnimation();
        
    }

    // Use this for initialization
    void Start() {
        mainCamera = FindObjectOfType<MainCamera>();
        screenMessage = FindObjectOfType<OnScreenText>();
        hungerBar = FindObjectOfType<HungerBar>();
        inspectionButtons = GameObject.FindGameObjectsWithTag("InspectionButton");
        HideInspectionOptions();
        mushroomCountText = GameObject.FindGameObjectWithTag("MushroomCountText").GetComponent<Text>();
        levelInfo = FindObjectOfType<CurrentLevelInfo>();
        desiredMushroom = levelInfo.desiredMushroom;
        levelInfo.mushroomCount = 0;
        levelInfo.pickedMushrooms = new Dictionary<MushroomScript.MushroomName, int>();
    }

    void HideInspectionOptions()
    {
        for(int i = 0; i < inspectionButtons.Length; i++)
        {
            inspectionButtons[i].SetActive(false);
        }
    }

    void ShowInspectionOptions()
    {
        for (int i = 0; i < inspectionButtons.Length; i++)
        {
            inspectionButtons[i].SetActive(true);
        }
    }


	
	// Update is called once per frame
	void Update () {
        if(gameState == GameState.PICKING)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHit;
                mousePositionStart = Input.mousePosition;
                mousePositionHold = mousePositionStart;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out rayHit, 200))
                {
                    hitObject = rayHit.collider.gameObject;
                    Debug.Log(hitObject.name);
                    
                }else
                {
                    hitObject = null;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if(hitObject == null)
                {
                    return;
                }
                //For some reason it treats mouse position as a vector 3 so I can't create mouseDifference in 1 line
                Vector2 mouseDifference = Input.mousePosition;
                mouseDifference -= mousePositionStart;
                mouseDifference.x = Mathf.Abs(mouseDifference.x);
                mouseDifference.y = Mathf.Abs(mouseDifference.y);

                if (mouseDifference.y > swipeThreshold && hitObject.GetComponent<GrassScript>() != null)
                {
                    GrassScript objectScript = hitObject.GetComponent<GrassScript>();
                    objectScript.PullGrass();
                    grassPlucked++;
                }
                else if (mouseDifference.x > swipeThreshold && hitObject.GetComponent<LeafScript>() != null)
                {
                    LeafScript objectScript = hitObject.GetComponent<LeafScript>();
                    objectScript.BrushLeaf(true);
                    leavesBrushed++;
                }
                else if (hitObject.GetComponent<MushroomScript>() != null)
                {
                    currentMushroom = hitObject.GetComponent<MushroomScript>();
                    currentMushroom.BringToCamera();
                    gameState = GameState.INSPECTING;
                    ShowInspectionOptions();
                }hitObject = null;
            }
        }else if(gameState == GameState.INSPECTING)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mouseDifference = Input.mousePosition;
                mouseDifference -= mousePositionHold;
                float turnSpeed = 0.2f;
                Vector3 rotatePosition = new Vector3(mouseDifference.y * turnSpeed, 0, mouseDifference.x * turnSpeed );
                currentMushroom.Rotate(rotatePosition);   
            }

            mousePositionHold = Input.mousePosition;
        }
        
	}
}
