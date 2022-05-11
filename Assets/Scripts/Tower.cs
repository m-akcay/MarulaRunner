using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private readonly Vector3 FORWARD_VEC = new Vector3(0, 0, 0.01f);
    private readonly Vector3 RIGHT_VEC = new Vector3(0.01f, 0, 0);

    [SerializeField] private bool finished = false;
    public const float SCALE = 4;

    [SerializeField] private GameObject powerUpButton = null;
    [SerializeField] private float height = 1;
    [SerializeField] private float speed = 10;
    private Transform mTransform = null;
    private Material mat = null;
    [SerializeField] private GameManager gm = null;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        powerUpButton = GameObject.Find("PowerUpButton");
        mTransform = GetComponent<Transform>();
        mat = GetComponentInChildren<Renderer>().material;
    }

    void Update()
    {
        if (finished)
            return;

        var posChange = mTransform.forward;

        if (Input.GetKey(KeyCode.A))
        {
            posChange -= mTransform.right * 0.5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            posChange += mTransform.right * 0.5f;
        }

        mTransform.position += posChange * speed * Time.deltaTime;
    }

    public void changeHeight(float amount)
    {
        var position = mTransform.position;

        float newY = height + amount;
        if (newY > 0)
        {
            mTransform.localScale = new Vector3(1, newY, 1);

            float scaledY = newY * SCALE;
            
            position.y = scaledY * 0.5f;
            mTransform.position = position;

            mat.mainTextureScale = new Vector2(1, scaledY);
            height = newY;
        }
        else // game over
        {
            finished = true;
            var pos = mTransform.position;
            mTransform.position = new Vector3(pos.x, 0.25f, pos.z);
            gm.gameOver();
        }
    }

    // unused and not tested
    private IEnumerator rotateToTargetInTSeconds(float t, float targetRotY)
    {
        float angleDiff = targetRotY - mTransform.rotation.eulerAngles.y;
        int stepCount = 60;
        float stepSize = angleDiff / stepCount;
        float rotInterval = t / stepCount;

        while (true)
        {
            var currentRot = mTransform.rotation;

            if (Mathf.Abs(angleDiff) > stepSize)
            {
                float eulerY = currentRot.eulerAngles.y + stepSize;
                mTransform.rotation = Quaternion.Euler(0, eulerY, 0);
                angleDiff -= stepSize;
            }
            else
            {
                mTransform.rotation = Quaternion.Euler(0, targetRotY, 0);
                break;
            }

            yield return new WaitForSecondsRealtime(rotInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // change direction when got into a new path
            // not used yet
            StartCoroutine(rotateToTargetInTSeconds(0.5f, other.transform.rotation.eulerAngles.y));
        }
        else if (other.CompareTag("Collectible"))
        {
            var collectibleObj = other.GetComponent<CollectibleObject>();
            changeHeight(collectibleObj.effect);
            collectibleObj.particleEffect();
            collectibleObj.destroy();
        }
        else if (other.CompareTag("Obstacle"))
        {
            var obstacle = other.GetComponent<Obstacle>();
            obstacle.particleEffect();
            changeHeight(obstacle.effect);
            DroppedCube.getAvailableCube().dropToPosition(obstacle.droppedCubePosition(mTransform.forward), Mathf.Abs(obstacle.effect));
        }
        else if (other.CompareTag("Finish"))
        {
            finished = true;
            gm.successfulFinish(height);
        }
    }

    public void powerUp()
    {
        powerUpButton.SetActive(false);
        StartCoroutine(powerUpForXSecs(5));
    }

    private IEnumerator powerUpForXSecs(float x)
    {
        speed *= 2;
        yield return new WaitForSecondsRealtime(x);
        speed *= 0.5f;
    }

    private void OnDestroy()
    {
        Destroy(mat);
    }
}
