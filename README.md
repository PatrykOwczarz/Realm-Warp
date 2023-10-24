# Realm Warp
 
Using the video game Control as an inspiration I created an action game with an emphasis on telekinetic manipulation of the surrounding. I utilised the Unity game engine and wrote the game in C#.

# Notable Algorithms and Learnings:
## Telekinetic Gravity manipulation system:
- A system inspired by the game Control by Remedy Entertainment.
- An implementation in 2 parts, A raycast system and the main telekinesis controller.
- First a raycast would be shot from the hand of the player character to the crosshair position. If the object would be a valid object with the object tag "Telekinesis" then the object would be manipulated using the telekinesis controller
- The telekinesis controller system uses a finite state machine with 4 states: WAITING, LIFT, PULL and THROW. The system starts out in the WAITING state until the raycast system returns a valid object.
- The system then transitions to the LIFT state which lifts the object 2 units into the air and then transfers control to the pull state which pulls the object towards the players hand.
- The object would then levitate by the players hand awaiting player input. When the player presses the left mouse button the state transitions to THROW and the object would be propelled with a force based on the mass of the object being carried in the direction of the crosshair.
- Finally the state returns to WAITING for another object to be thrown.

Link to TelekinesisController.cs: https://github.com/PatrykOwczarz/Realm-Warp/blob/main/Realm%20Warp/Assets/Scripts/TelekinesisController.cs

## Force push
- A GameObject with a box collider was placed in front of the player.
- A script called ForcePush.cs was implemented which would activate and deactivate the collider of the aforementioned GameObject.
- To force push, the player would first have to pickup a dark realm orb and then press the Q key.
- The force push was simulated by activating the box collider which would apply a force to any objects caught in the box collider in a direction away from the player location. The collider would then deactivate unless Q is pressed again.

Link to ForcePush.cs: https://github.com/PatrykOwczarz/Realm-Warp/blob/main/Realm%20Warp/Assets/Scripts/ForcePush.cs

## Ragdolls
- Used Unities Ragdoll builder. Input each joint of the player model to create rigidbodies at each limb. Each rigidbody acts independently based on gravity. Unity would assign each limb/body part with a mass to simulate ragdoll behaviour.
- I wrote a script that would deactivate the animation manager and enable the rigidbodies of each joint, causing the model to enter ragdoll mode. Ragdoll would only be used when the enemy was defeated.

Link to Ragdoll.cs: https://github.com/PatrykOwczarz/Realm-Warp/blob/main/Realm%20Warp/Assets/Scripts/Ragdoll.cs

# Areas of Improvement:
- There is limited amounts of objects that can be picked up. This was due to several bugs encountered during development so the current implementation was a compromise to finish the main functionality on time for my deadline.
- The enemy AI was not very complex, following a simple state machine implementation which just followed the player if they entered their aggro radius.
- Some bugs with the ragdoll colision causing the enemy model to move in the opposite direction that they are supposed to.
- Level design could do with some work.

# Demo Video

Demo: https://www.youtube.com/watch?v=KxAylqFRk6Q
