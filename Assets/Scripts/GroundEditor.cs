using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEditor : MonoBehaviour
{
    private Transform getLastAddedPiece()
    {
        var allGroundPieces = GetComponentsInChildren<Transform>();
        return allGroundPieces[allGroundPieces.Length - 1];
    }

    [ContextMenu("Add forward")]
    public void addForward()
    {
        var lastPieceTransform = getLastAddedPiece();
        var gcube = Instantiate(lastPieceTransform.gameObject);
        // let unity handle the numbering
        gcube.name = "GCube";
        gcube.transform.position = lastPieceTransform.position + lastPieceTransform.forward * lastPieceTransform.localScale.z;
        gcube.transform.parent = lastPieceTransform.parent;
    }

    [ContextMenu("Turn right")]
    public void turnRight()
    {
        var lastPieceTransform = getLastAddedPiece();
        var gcube = Instantiate(lastPieceTransform.gameObject);
        var gCubeTransform = gcube.transform;

        gcube.name = "GCube";
        gcube.transform.Rotate(Vector3.up, 90);

        var pos = lastPieceTransform.position
            + lastPieceTransform.forward * lastPieceTransform.localScale.z * 0.5f     // end of the previous piece
            - gCubeTransform.right * gCubeTransform.localScale.x * 0.5f     // thickness of new piece
            - gCubeTransform.forward * lastPieceTransform.localScale.x * 0.5f;   // match the thickness of the previuos piece

        // shift right
        pos += gCubeTransform.forward * gCubeTransform.localScale.z * 0.5f;

        gcube.transform.position = pos;
        gcube.transform.parent = lastPieceTransform.parent;
    }

    /*
     * almost same with turnRight
     * rotation angle is -90 instead of 90
     * thickness of new piece is calculate by addition as we need to shift the piece in the local right direction
     */
    [ContextMenu("Turn left")]
    public void turnLeft()
    {
        var lastPieceTransform = getLastAddedPiece();
        var gcube = Instantiate(lastPieceTransform.gameObject);
        var gCubeTransform = gcube.transform;

        gcube.name = "GCube";
        gcube.transform.Rotate(Vector3.up, -90);

        var pos = lastPieceTransform.position
            + lastPieceTransform.forward * lastPieceTransform.localScale.z * 0.5f     // end of the previous piece
            + gCubeTransform.right * gCubeTransform.localScale.x * 0.5f     // thickness of new piece
            - gCubeTransform.forward * lastPieceTransform.localScale.x * 0.5f;   // match the thickness of the previuos piece

        // shift left
        pos += gCubeTransform.forward * gCubeTransform.localScale.z * 0.5f;

        gcube.transform.position = pos;
        gcube.transform.parent = lastPieceTransform.parent;
    }

}
