# Sprites & SpriteBatch
-	Sprites
-	SpriteBatch
-	Texture2D
-	Vector2

## Sprites
Sprites are 2D bitmaps that we will draw directly to the screen. Originally sprites were images that were loaded into a memory buffer and rendered separately from the background image, allowing sprites to be used as game objects that can be modified in memory and moved around on the screen, without effecting any other images, game objects, or the background. The name sprite is a reference to a spirit or ghost, which some cultures refer to as a sprite. The terminology is based on sprites originally using this temporary or temporal memory buffer and were displayed sort of floating above the background images, like a ghost. While this isn’t exactly how they work today, 2D bitmap images are still referred to as sprites.

Designers use a sheet of sprites, called a spritesheet to hold multiple sprites on a single image. Often a single spritesheet will contain all the sprites needed for a game. We will use a couple of freely distributed spritesheets and a few that I created specifically for our games. Using a single spritesheet image allows us to simply load one image, then write simple functions to pick out the images based on their x/y grid coordinates on the spritesheet, as well as the border size between them. For example, maybe we want an 8x8 image from the 2nd column and the 3rd row, and there is a 1-pixel border between them. So maybe we want an 8x8 image that starts at the (x,y) coordinates of ((1+8)*2, (1+8)*3).

Many of the older games were built using only sprites for images. 3D games often use sprites for 2D drawings and text. For our retro arcade games, will use sprites for all our graphics. Not only does this remove the need to develop 3D models, but it also allows us to quickly box out our games using simple rectangles to work out the game play and game features, then skin our games using our own custom sprites or use any of the millions of free sprites available online.

## Animating Sprites

A series of sprites can be used to animate a character’s movement such walking, jumping, or an explosion. The animation is created by drawing a series of sprites as individual frames, sort of like an animated gif or an animated flipbook. For now, we will be focusing on simple sprites and not animating them. Later we will investigate animating our sprites. There are numerous online tools to help you work with animates sprites. However, for our needs we will roll out or own animation methods from scratch so we can see how this works, and we will discuss a few NuGet packages that can make it easier to work with animated sprites. 

## SpriteBatch
When working with sprites, we will be using the SpriteBatch class. SpriteBatch is exactly what it sounds like, a way batch the rendering of sprites in your games to optimize performance. While a definition of a sprite can be hard to nail down, as we tried in the section above, it typically refers to small bitmap image that are rendered as individual game objects that can be manipulated independently without effecting other sprites, images, the background, etc. If that is how we define them, then SpriteBatch can work with more than just sprites. 

The SpriteBatch class is a helper class for drawing images in optimized batches. For our needs, SpriteBatch will allow you to draw images one at a time to memory, then render them from memory to your GraphicsDevice, GPU, and screen. This is implemented by executing a call to SpriteBatch.Begin() to start, executing one or more SpriteBatch.Draw() methods, and finally executing SpriteBatch.End() to flush them from memory to the screen, then resetting to begin the next batch. 

The way it works is that Begin() initializes the default state and optional parameters override the defaults to set parameters such as the sort mode, blend state, stencil depth, custom effects, and others. The parameters are something we can ignore for now, they are mostly advanced features. Each Draw() method creates a new SpriteBatchItem and pools them in sets of 64 items, reusing already allocated items and extending the pool size in chunks of 64 as needed. 

Note that the default pool size is 256, but you can initialize it to whatever size you want. If for example, you are only using say 4 images in the SpriteBatch, you could initialize the size to 4. SpriteBatch will set it to the first chunk of 64. So, you might as well use the default or if you prefer to reduce the size, set it to 64. Or you could just leave off the optional parameter and with the default of 256. We will ignore this, but I wanted to mention what that parameter is used for. To set it to 64, you would code it as follows.   

    _spriteBatch = new SpriteBatch(GraphicsDevice, 64);	
    
The End() method completes the process by sorting and grouping batch items into optimized sets based on the provided parameters from the Begin() method. A SpriteBatcher applies your sort mode and custom effects, then batches the SpriteBatchItems to the GraphicsDevice, which in turn sends the results to your GPU and finally to your screen.

Sprite batching is fundamental to 2D game develop and the concept has been around for a long time. Before MonoGame and XNA, C# developers had to build their own sprite batch classes and adopted Begin(), Draw(), and End() methods.  Early versions of DirectX supported sprite batching and of course later XNA and MonoGame did as well. 

One way to think about this, is that drawing images to the screen has significant overhead between the CPU and GPU hardware. Pushing a single sprite to the GPU is nearly as efficient as pushing a batch of sprites to the GPU, so that when you need to draw multiple sprites to the screen, it is often better to draw them quickly and efficiently to memory, then draw a single optimized batch to the screen.   

MonoGame supports both a deferred and an immediate SpriteBatch mode, as well as a few others. In immediate mode, SpriteBatch will immediately draw the sprites to the screen, which can be faster than deferred mode in some cases. However, if you need to have multiple SpriteBatch objects in your game, which is common, they may interfere with each other. The key here is that in immediate mode, you will need to insure that each Begin() and End() is called for one SpriteBatch before the next SpriteBatch.Begin() is called for the next. If you do not, they will likely interfere with each other. In that case, you will need to use deferred mode. 

MonoGame uses deferred mode by default. To keep things simple, we will use this default in our games. If you want to experiment with immediate mode, you can set it when calling SpriteBatch.Begin() and setting the SortMode.
    _spriteBatch.Begin(SpriteSortMode.Immediate); 
    
### Testing SpriteBatch Performance
-	Remove the FPS and let it run wide open
-	Time the game loop functions
-	Compare with different initialization values… 64 vs say 1088. 

### Going Deeper
If you have the desire and skill, you could develop your own sprite batch class. Creating your own would require building what are called quads. Quads are 4 vertices.
