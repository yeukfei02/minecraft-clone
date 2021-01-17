using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> gameObjectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject camera = GameObject.Find("Camera");

        freezeCameraZRotation(camera);

        KeyBoardCameraControl(camera);

        MouseMoveCameraControl(camera);

        ClickGameObject();

        RightClickCreateGameObject(gameObjectList, camera);
    }

    private void freezeCameraZRotation(GameObject camera) {
        camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y, 0);
    }

    private void KeyBoardCameraControl(GameObject camera) {
        float speed = 0.3f;

        if (Input.GetKey(KeyCode.W)) {
            Debug.Log("w was pressed");
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + speed);
        }
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("a was pressed");
            camera.transform.position = new Vector3(camera.transform.position.x - speed, camera.transform.position.y, camera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S)) {
            Debug.Log("s was pressed");
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            Debug.Log("d was pressed");
            camera.transform.position = new Vector3(camera.transform.position.x + speed, camera.transform.position.y, camera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("space was pressed");
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + speed, camera.transform.position.z);
        }
    }

    private void MouseMoveCameraControl(GameObject camera) {
        float sensitivity = 3f;
        camera.transform.RotateAround(camera.transform.position, camera.transform.up, -Input.GetAxis("Mouse X") * sensitivity);
        camera.transform.RotateAround(camera.transform.position, camera.transform.right, -Input.GetAxis("Mouse Y") * sensitivity);
    }

    private void ClickGameObject() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse left click");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                Debug.Log("Hit = " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.name != null && hitInfo.transform.gameObject.name != "Ground") {
                    Debug.Log("Add gameObject to list");
                    gameObjectList.Add(hitInfo.transform.gameObject);
                }
            } else {
                Debug.Log("No hit");
            }
        }
    }

    private void RightClickCreateGameObject(List<GameObject> gameObjectList, GameObject camera) {
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Mouse right click");

            if (gameObjectList != null && gameObjectList.Count > 0) {
                Vector3 newGameObjectPosition = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 5);
                Instantiate(gameObjectList[0], newGameObjectPosition, gameObject.transform.rotation);
                gameObjectList.RemoveAt(0);
            }
        }
    }
}
