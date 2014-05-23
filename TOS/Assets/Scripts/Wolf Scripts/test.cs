using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class test :AIPath {
		
		/** Animation component.
		 * Should hold animations "awake" and "forward"
		 */
		
		/** Minimum velocity for moving */
		public float sleepVelocity = 0.4F;
		
		/** Speed relative to velocity with which to play animations */
		public float animationSpeed = 0.2F;
		
		/** Effect which will be instantiated when end of path is reached.
		 * \see OnTargetReached */
		public GameObject endOfPathEffect;

        public bool canFollow;
		
		public new void Start () {
			
			
			//Call Start in base script (AIPath)
			base.Start ();
		}
		
		/** Point for the last spawn of #endOfPathEffect */
		protected Vector3 lastTarget;
		
		/**
		 * Called when the end of path has been reached.
		 * An effect (#endOfPathEffect) is spawned when this function is called
		 * However, since paths are recalculated quite often, we only spawn the effect
		 * when the current position is some distance away from the previous spawn-point
		*/
		public override void OnTargetReached () {
			
			if (endOfPathEffect != null && Vector3.Distance (tr.position, lastTarget) > 1) {
				GameObject.Instantiate (endOfPathEffect,tr.position,tr.rotation);
				lastTarget = tr.position;
			}
		}
		
		public override Vector3 GetFeetPosition ()
		{
			return tr.position;
		}
		
		protected new void Update () {
			
			//Get velocity in world-space
			Vector3 velocity;
            if (canFollow)
            {
			
				//Calculate desired velocity
				Vector3 dir = CalculateVelocity (GetFeetPosition());
				
				//Rotate towards targetDirection (filled in by CalculateVelocity)
				RotateTowards (targetDirection);
				
				dir.y = 0;
				if (dir.sqrMagnitude > sleepVelocity*sleepVelocity) {
					//If the velocity is large enough, move
				} else {
					//Otherwise, just stand still (this ensures gravity is applied)
					dir = Vector3.zero;
				}
				
				if (navController != null) {
	#if FALSE
					navController.SimpleMove (GetFeetPosition(), dir);
	#endif
				} else if (controller != null)
					controller.SimpleMove (dir);
				else
					Debug.LogWarning ("No NavmeshController or CharacterController attached to GameObject");
				
				velocity = controller.velocity;
			} else {
				velocity = Vector3.zero;
			}
			
			}
		}
	
