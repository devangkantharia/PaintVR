using UnityEngine;
using System.Collections.Generic;
using Leap.Unity;
using PaintUtilities;

public class Paint : MonoBehaviour {

	public PolyHand right;

	// Leap motion api to detect pinch gestures
	public PinchDetector[] pinchDetectors;

	// Paint material / shader
	public Material[] materials;

	// Paint line thickness
	public float thickness;

	// Paint line resolution
	public int resolution;

	// Paint lines
	private PaintLine[] paintLines;

	void Awake() {
		thickness = 0.0015f;
		resolution = 3;
	}

	void Start() {
		paintLines = new PaintLine[pinchDetectors.Length];
		for (int i = 0; i < pinchDetectors.Length; i++) {
			paintLines[i] = new PaintLine(this);
		}
	}

	void Update() {
		// Update each pinch detector (one per hand)
		int index = 0;
		foreach (PinchDetector pd in pinchDetectors) {
			if (pd.DidStartPinch) {
				paintLines [index].InitPaintLine ();
			}
			if (pd.DidRelease) {
				paintLines [index].EndPaintLine ();
			}
			if (pd.IsHolding) {
				
				paintLines [index].UpdatePaintLine (pd.Position);
			}
		}
	}
}