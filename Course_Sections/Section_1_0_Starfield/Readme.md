This is a very simple demo of a retro 1980's arcade starfield. I created this based on the starfield used in Galaga and use this going forward in my Galaga clone. 

This project demostrates a number of important features.

* Game Loop
  *  Initialize() 
  *  LoadContent()
  *  Update()
  *  Draw() 
* Graphics Device
* GameTime
* Game Services
* Game Components
* Frame Counters
* 2D Textures
* Screen Size & Back Buffer
* SpriteBatch
  * SpriteBatch Begin(), Draw(), and End(_.
  * Drawing rectangle primitives
* Creating random numbers, colors, and cordinates.
* 2D Vectors
* Colors
* Velocity

If you are just beginning with game development and not familiar with the terms used above, I wold recommend you start with the MonoGame samples at MonoGame.org. While those lack detail, they are a great way to introduce you to the concepts, terminology, etc. and will go a long way to getting you familiar with things. Here, I will try to expand on points not often mentioned elsewhere, but that I think is important. We just do not have the time or space here to explain every term in detail... and I don't think you would want to have to read through it all here anyway.  

### Game Loop
The game loop is the primary structure of all games, not just in C# and MonoGame, but in any language and any game framework or engine. The best way to think about this is as an animated film. Each frame of the animation must be drawn in memory and rendered to the screen. In order to similuate movement, each character, the camera, etc. needs to be adjusted before displaying the next frame. If for example, you are flying a spaceship, each frame needs to calaculate the next position of your ship in memory before it can be rendered to the screen. Your game loop will include methods for Initialize(), Update(), and Draw(). Initialzie() will of course run just once at the start, and Update() and Draw() will be executed in a loop. Update() will run once per frame, but Draw() may or maynot. To control framerate, the Draw() method can be skipped over. We only exit the loop when the game has been terminated. The game loop is the fundamental structure of a game and everything else we do will be based on and dependent on this loop. 

All games regardless of which language you use, will have a game loop and an equivalent to Initialize(), Update(), and Draw() methods. In many languages the Draw() method is named Render(). That can be confusing to new developers because your game assets will be drawn to memory buffer called the back-buffer. Once the back-buffer has been drawn, it will then be rendered to the screen. It is important to keep this in mind. This means that at 60 frames per second, you will have .0167 seconds to do all of the necessary math, polling, and drawing your next frame. While this doesn't sound like much time, it is. Even so, you need to be aware of how this works and make good choices in where you will handle parts of the game. For example, you would not want use the Update() or Draw() methods to load game assets from a file. Also, it is very important to seperate your Update() and Draw() logic correctly. For example, you do not want to calculate an assets position in Draw(), as Draw() may be skipped if your framerate starts to lag. However, Update() will run once per frame. So you should only update your assets location in the Update() function. If not, when your framerate drops, your game will slow down. 

MonoGame handles drawing images to via SpriteBatch. SpriteBatch is pretty much what it sounds like, a way to batch drawing images to the back-buffer and rendering them to the screen. Don't let the term "sprite" confuse you. Mondern applications no longer use sprites, which were a special memory buffer, but the term has stuck around. We will go into details about what these areas in future setions. For now, I think it is important to understand some of these destinctions so you can begin to dig into example code. We will dig deeply into SpriteBatch in later sections. We can ignore the details for now. 

#### Initialize
In this function we want to handle all of the initialization of the game. This would include things like setting the high and width of our screen, registering any game components and services, etc. You can also load your content here, and often you will see this. However, the preference is to handle loading content in the LoadContent() method. However, that is not always possible or feasable. A common example is when I want to register a game component and need to pass in content. In that case, I will need to either load the content in Initialize() and then add my game component, passing in the content, or I will need to register my game component in the LoadContent() method. Either is really fine. I am not aware of any advantages one way or the other. 

There is one thing to consider. When you execute the base Initialize(), it will initialize your graphics device. Therefore, it's a good idea to wait to load your contact until after the Initialization() method has been executed and the base.Initialize() has been ran. This also means that all of your game components will have been iniitalized as well, before your LoanContent() method is executed. I'd recommend just using the methods as intended for now. Once you are more familiar with them, you can decide how best to use them. 

#### Update
Here we poll for user interaction (keyboard, mouse, gamepad, joystick, etc.) and handle updating any our game assets, components, etc. For example, if we have a gamepad that moves our player, here would we calculate that movement. We might have logic that is tracking the players speed (velocity) and position. We'd execute the math to move the player here. But we will render the player's image to the screen in the next method. 

#### Draw
The Draw() method is where we draw our game assets to memory, a back-buffer. Once everything has been drawn, MonoGame will render the the frame to the screen. 

