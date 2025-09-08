using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public Transform camTransform;
    private bool isJumping = false;

    void Update()
    {
        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.W)) v += 1;
        if (Input.GetKey(KeyCode.S)) v -= 1;
        if (Input.GetKey(KeyCode.A)) h -= 1;
        if (Input.GetKey(KeyCode.D)) h += 1;

        Vector3 moveDir = camTransform.forward * v + camTransform.right * h;
        moveDir.y = 0f;
        moveDir.Normalize();

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(Jump());
        }
    }

    System.Collections.IEnumerator Jump()
    {
        float jumpTime = 0.3f;
        float timer = 0f;

        while (timer < jumpTime)
        {
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        timer = 0f;
        while (timer < jumpTime)
        {
            transform.Translate(Vector3.down * jumpHeight * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isJumping = false;
    }
}
