### Controls:
W / Up arrow :  Accelerate.
A-D / Left - Right Arrow : Rotate.
Left Click / Space : Shoot.

# Asteroids
Asteroids game implementation for technical interview in Loog Guitars

On this project my main concern was keeping the code clean and readable while maintaining performance, Here I explain some of the things that the client expected and how I approached the solution:

### Wraparound:
In this case I created the Screen Bounds script, which generates a BoxCollider2D component the size of the camera view, then, each object that needs to wrap HAS to have the component WrapTrigger.
When a Object with a collider2D leaves the ScreenBounds BoxCollider2D trigger, it checks if that object has the <b>WrapTrigger</b> component, if it has it, then it applies the correct position.

The ScreenBounds has some Offset Fields for better detection of the BoxCollider2D trigger and so that the objects that wrap don't appear in your view right in the other side of the screen

![image](https://user-images.githubusercontent.com/61606117/230508443-5072a445-f592-431e-b38b-866da6cfb4aa.png)


### Player Movements:
The player movements are based on physics for it to have the classic hard to control movement of a 2D spaceship.

![image](https://user-images.githubusercontent.com/61606117/230508766-e1db3263-a0f2-4c3c-8775-fd2a8bec3e46.png)


### Optimization:
The spawning of Asteroids is all done in the central AsteroidsSpawner script, which utilizes ObjectPools, which optimizes the spawning of asteroids instantiating them once and then activating and deactivating asteroids when needed.

![image](https://user-images.githubusercontent.com/61606117/230509133-638d1acf-3810-4dbc-99e5-acdc38a88e78.png)

Another thing I did that is worth noticing is that I deactivated some of the collisions in the Layer Collision Matrix in Project settings, this reduces some of the collision calculation, but I only did it on the collisions that I was sure would never happen or wouldn't matter (Player to Player, Player to Bullet, etc.)

![image](https://user-images.githubusercontent.com/61606117/230509772-71cafacd-cb46-406b-ac6f-f02b719fd676.png)


Those are the most interesting things I did on this project, most of the other things I did where some basic setup and things like detecting things on OnTriggerEnter2D, etc.  


Libraries Used:
- Unity's new Input System
- DoTween
- Universal RP
- TextMeshPro
