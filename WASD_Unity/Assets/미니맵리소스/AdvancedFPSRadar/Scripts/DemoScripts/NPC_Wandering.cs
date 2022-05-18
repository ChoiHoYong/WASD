using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class NPC_Wandering : MonoBehaviour
{
    public float speed = 1;
    public float directionChangeInterval = 1;

    CharacterController controller;
    float heady;
    float headx;
    float headz;
    Vector3 targetRotation;
    private bool isRotating = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        if (isRotating == true)
            transform.Rotate(targetRotation * Time.deltaTime * directionChangeInterval, Space.Self);

        if(isRotating == false)
            controller.SimpleMove(transform.forward * speed);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            isRotating = false;
            yield return new WaitForSeconds(directionChangeInterval);
            isRotating = true;
            yield return new WaitForSeconds(directionChangeInterval);
            isRotating = false;
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        heady = Random.Range(-20, 20);

        targetRotation = new Vector3(0, heady, 0);
    }
}