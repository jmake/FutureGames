# FutureGames

```
var createScene = function () 
{

    var scene = new BABYLON.Scene(engine);

    var URL = "https://raw.githubusercontent.com/jmake/FutureGames/Models/"; 
    var GLB = "test03a.glb"; 
    BABYLON.SceneLoader.Append(URL, GLB, scene, function (newMeshes) {
        scene.createDefaultCameraOrLight(true);
        scene.activeCamera.attachControl(canvas, false);
        scene.activeCamera.alpha += Math.PI; // camera +180°
    });

    return scene;
}
```

```
    var URL = "https://raw.githubusercontent.com/jmake/FutureGames/Models/"; 
    var GLB = "test03a.glb"; 
    var BYL = "scene.babylon";
```



```


```

