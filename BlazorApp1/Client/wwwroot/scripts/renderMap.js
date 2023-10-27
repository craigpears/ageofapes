function renderMap(lootingLocations) {
    const canvas = document.getElementById("myCanvas");
    const ctx = canvas.getContext("2d");

    var gridWidth = 7200; // Spreads out the points more or less on the x axis.  Lower number = more spread out 
    var imageMarginLeft =  600; 
    var imageMarginRight = 1200;
    var imageMarginBottom = 700;
    var imageMarginTop = 1500;
    var gridHeight = 7200; // Spreads out the points more or less on the y axis.  Should equal the biggest yCoordinate visible?
    
    var canvasWidth = gridWidth + imageMarginRight + imageMarginLeft;
    var canvasHeight = gridHeight + imageMarginBottom + imageMarginTop;
    
    canvas.width = canvasWidth;
    canvas.height = canvasHeight;
    
    console.log("rendering locations");
    console.log(window.screen);
    console.log(canvas);
    console.log(canvas.height);
    console.log(canvas.width);
    

    lootingLocations.forEach(function(lootingLocation) {

        var canvasCoordinates = toCanvasCoordinates(lootingLocation.xCoordinates, lootingLocation.yCoordinates, canvasHeight, imageMarginLeft, imageMarginBottom);
        var xPos = canvasCoordinates.x;
        var yPos = canvasCoordinates.y;
        console.log("rendering location at " + xPos + ", " + yPos);
        console.log(lootingLocation);
        
        var totalResources = lootingLocation.ironAmount + lootingLocation.foodAmount;
        var resourcesPerPixel = 250000;
        var cityRadius = totalResources / resourcesPerPixel;
        var minimumRadius = 20;
        var minimumResources = 500000;
        var highResources = 5000000;
        var mediumResources = 250000;
        var lowDefence = 5000;
        
        // TODO: Move this logic into the back end and return them as enums so it can be exported, should only be rendering here
        if(totalResources < minimumResources || lootingLocation.recovering)
        {
            ctx.fillStyle = "black";
            cityRadius = minimumRadius;
        }
        else if(lootingLocation.armyCount < lowDefence && totalResources < highResources)
        {
            ctx.fillStyle = "white";
            cityRadius += minimumRadius;
        }
        else if(totalResources > highResources)
        {
            ctx.fillStyle = "red";
            cityRadius = totalResources / highResources;
            cityRadius *= 2;
            cityRadius += minimumRadius * 1.5;
        }
        else if(totalResources > mediumResources)
        {
            ctx.fillStyle = "orange";
            cityRadius += minimumRadius * 0.75;
        }
                
        
        ctx.beginPath();
        ctx.arc(xPos, yPos, cityRadius, 0, 2 * Math.PI);
        //ctx.rect(xPos, yPos, 100, 200);
        ctx.fill();
        ctx.stroke();

        ctx.font = "80px Arial";
        ctx.fillStyle = "white";
        //ctx.fillText(lootingLocation.playerName + ": " + lootingLocation.xCoordinates +", " + lootingLocation.yCoordinates, xPos, yPos);
        
        var resourcesMillions = Math.floor(totalResources / 1000000);
        if(resourcesMillions >= 15)
        {
            //ctx.fillText(lootingLocation.xCoordinates + ", " + lootingLocation.yCoordinates, xPos, yPos);
            ctx.fillText(resourcesMillions + "M", xPos - 40, yPos + 20);
        }
    });
    
    console.log("drawing debugging margins");
    ctx.font = "80px Arial";
    ctx.fillStyle = "white";
    ctx.strokeStyle = "white";
    ctx.lineWidth = 10; 
        
    // Draw the margins for debugging
    var debug = false;
    var drawWholeGrid = false;
    var drawDebugCoordinates = false;
    if(debug) {
        // Draw outer margin lines
        /*
        ctx.beginPath();
        ctx.moveTo(leftMarginBottom.x, leftMarginBottom.y);
        ctx.lineTo(leftMarginTop.x, leftMarginTop.y);
        ctx.stroke();
        
         
        ctx.beginPath();
        ctx.moveTo(canvasWidth - imageMarginRight, 0);
        ctx.lineTo(canvasWidth - imageMarginRight, canvasHeight);
        ctx.stroke();

        ctx.beginPath();
        ctx.moveTo(0, canvasHeight - imageMarginBottom);
        ctx.lineTo(canvasWidth, canvasHeight - imageMarginBottom);
        ctx.stroke();

        ctx.beginPath();
        ctx.moveTo(0, imageMarginTop);
        ctx.lineTo(canvasWidth, imageMarginTop);
        ctx.stroke();
        */

        // Draw another set of lines every x pixels inside the margins, use a number 7200 is divisible by for an even grid
        /*
        for(var i = 0; i < gridWidth; i += 450)
        {
            ctx.beginPath();
            ctx.moveTo(imageMarginLeft + i, 0);
            ctx.lineTo(imageMarginLeft + i, canvasHeight);
            ctx.stroke();

            ctx.beginPath();
            ctx.moveTo(0, canvasHeight - imageMarginBottom - i);
            ctx.lineTo(canvasWidth, canvasHeight - imageMarginBottom - i);
            ctx.stroke();
        }
        */
        
        // Draw another set to see how the canvas co-ordinates change with the y position
        for(var i = 0; i <= gridWidth; i += 450)
        {
            var verticalStart = toCanvasCoordinates(i, 0, canvasHeight, imageMarginLeft, imageMarginBottom);
            var verticalEnd = toCanvasCoordinates(i, 7200, canvasHeight, imageMarginLeft, imageMarginBottom);
            var horizontalStart = toCanvasCoordinates(0, i, canvasHeight, imageMarginLeft, imageMarginBottom);
            var horizontalEnd = toCanvasCoordinates(7200, i, canvasHeight, imageMarginLeft, imageMarginBottom);

            ctx.beginPath();
            ctx.moveTo(verticalStart.x, verticalStart.y);
            ctx.lineTo(verticalEnd.x, verticalEnd.y);
            ctx.stroke();

            ctx.beginPath();
            ctx.moveTo(horizontalStart.x, horizontalStart.y);
            ctx.lineTo(horizontalEnd.x, horizontalEnd.y);
            ctx.stroke();
        }
        
        if(drawDebugCoordinates)
        {
            ctx.fillStyle = "red";
            
            var launchpad = toCanvasCoordinates(3600, 3700, canvasHeight, imageMarginLeft, imageMarginBottom);
            var left = toCanvasCoordinates(2700, 3600, canvasHeight, imageMarginLeft, imageMarginBottom);
            var up = toCanvasCoordinates(3600, 4500, canvasHeight, imageMarginLeft, imageMarginBottom);
            var right = toCanvasCoordinates(4500, 3600, canvasHeight, imageMarginLeft, imageMarginBottom);
            var bottom = toCanvasCoordinates(3600, 2750, canvasHeight, imageMarginLeft, imageMarginBottom);

            var gridStart = toCanvasCoordinates(0, 0, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridEnd = toCanvasCoordinates(gridWidth, gridHeight, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridTopLeft = toCanvasCoordinates(0, gridHeight, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridBottomRight = toCanvasCoordinates(gridWidth, 0, canvasHeight, imageMarginLeft, imageMarginBottom);

            var gridMidLeft = toCanvasCoordinates(0, gridHeight / 2, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridMidTop = toCanvasCoordinates(gridWidth / 2, gridHeight, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridMidRight = toCanvasCoordinates(gridWidth, gridHeight / 2, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridMidBottom = toCanvasCoordinates(gridWidth / 2, 0, canvasHeight, imageMarginLeft, imageMarginBottom);
            var gridMiddle = toCanvasCoordinates(gridWidth / 2, gridHeight / 2, canvasHeight, imageMarginLeft, imageMarginBottom);

            var grindLocation = toCanvasCoordinates(2302, 1022, canvasHeight, imageMarginLeft, imageMarginBottom);
            var q80Location = toCanvasCoordinates(648, 5651, canvasHeight, imageMarginLeft, imageMarginBottom);
            var elbatlanLocation = toCanvasCoordinates(6827, 4332, canvasHeight, imageMarginLeft, imageMarginBottom);
            var tfiOscarLocation = toCanvasCoordinates(4433, 6762, canvasHeight, imageMarginLeft, imageMarginBottom);
            var bottomLeftLocation = toCanvasCoordinates(1800, 1800, canvasHeight, imageMarginLeft, imageMarginBottom);
            
            
            ctx.beginPath();
            ctx.fillText(0 + ", " + (gridHeight / 2), gridMidLeft.x + 20, gridMidLeft.y - 20);
            ctx.moveTo(gridMidLeft.x, gridMidLeft.y);
            ctx.lineTo(gridMidRight.x, gridMidRight.y);
            ctx.stroke();

            ctx.fillText((gridWidth / 2) + ", " + (gridHeight / 2), gridMiddle.x + 20, gridMiddle.y - 20);

            ctx.beginPath();
            ctx.moveTo(gridMidTop.x, gridMidTop.y);
            ctx.lineTo(gridMidBottom.x, gridMidBottom.y);
            ctx.stroke();

            // Draw some lines of well known co-ordinates for testing
            markCrossHairs(ctx, grindLocation.x, grindLocation.y); // Should be just inside of the TWU area
            markCrossHairs(ctx, q80Location.x, q80Location.y); // Should be in the red q80 area in the top left, just at the bottom edge
            markCrossHairs(ctx, elbatlanLocation.x, elbatlanLocation.y); // Should be on the right of the map, in the brown area above H2H
            markCrossHairs(ctx, tfiOscarLocation.x, tfiOscarLocation.y); // Should be at the top of the map, north-west of TFI and above the purple area almost touching the topmost brown patch.
            markCrossHairs(ctx, bottomLeftLocation.x, bottomLeftLocation.y); // Should be the supply station in the bottom left, dead on the gray square above the red patch
            
            // Draw some markers for the launchpad and innermost circle as reference points for testing
            drawCircle(ctx, launchpad.x, launchpad.y);
            drawCircle(ctx, left.x, left.y);
            drawCircle(ctx, up.x, up.y);
            drawCircle(ctx, right.x, right.y);
            drawCircle(ctx, bottom.x, bottom.y);
            ctx.fillStyle = "white";
        }

        if(drawWholeGrid)
        {
            console.log("drawing debugging grid");

            for (var gridxPos = 100; gridxPos < gridWidth; gridxPos += 600)
                for (var gridyPos = 100; gridyPos < gridHeight; gridyPos += 600) {
                    var canvasPos = toCanvasCoordinates(gridxPos, gridyPos, canvasHeight, imageMarginLeft, imageMarginBottom);
                    ctx.beginPath();
                    ctx.fillText(gridxPos + ", " + gridyPos, canvasPos.x, canvasPos.y);
                    ctx.fillText(canvasPos.x + ", " + canvasPos.y, canvasPos.x, canvasPos.y + 100);
                    ctx.arc(canvasPos.x, canvasPos.y, 10, 0, 2 * Math.PI);
                    ctx.fill();
                }
        }
        
    }
    
}

function toCanvasCoordinates(gridxPos, gridyPos, canvasHeight, imageMarginLeft, imageMarginBottom)
{
    // For some reason the grid is more stretched out at the bottom and denser at the top, so squash the x co-ordinates the further up you go
    var pushRatio = 0.1;
    var gridSize = 7200;
    var distanceToMiddle = (gridSize / 2) - gridxPos; // This should go negative past the middle to make the middle the center of the pull
    var percentageToMiddle = distanceToMiddle / (gridSize / 2); // Should be between 1 for the left, 0 for the middle, -1 for the right
    var distanceToMiddleEffect = 1.5;
    var distanceToMiddleModifier = percentageToMiddle * distanceToMiddleEffect; // Make the effect weaker the closer you get to the middle
    var pushAmount = gridyPos * pushRatio * distanceToMiddleModifier;
    
    var canvasX = (gridxPos + pushAmount) + imageMarginLeft;
    // Convert from 0,0 being bottom left in game to top left on the canvas
    // grids seem to be denser in y values the higher you go up.  So the grid sections need to be bigger at the bottom and get smaller towards the top.
    // The very top squares seem to be roughly half the size of the very bottom
    var yBias = 0.3;
    var yMultiplier = 1 + yBias - (gridyPos / (7200 / yBias)); // Get a multiplier between +1.5 and +1.0 to multiply the grid square sizes
    var gridBottomLeftCanvasY = canvasHeight - imageMarginBottom;
    if(gridxPos === 3600) console.log(yMultiplier);
    
    var canvasY = gridBottomLeftCanvasY - (yMultiplier * gridyPos);
    //console.log(gridBottomLeftCanvasY + " - " + gridyPos + " * " + yMultiplier + " => " + canvasY);
    return { "x": canvasX, "y": canvasY };
}

function multiplyMatrix(a, b) {
    // https://math.stackexchange.com/questions/1914806/converting-map-coordinates-of-a-rotated-grid
    // https://stackoverflow.com/questions/27205018/multiply-2-matrices-in-javascript
    var aNumRows = a.length, aNumCols = a[0].length,
        bNumRows = b.length, bNumCols = b[0].length,
        m = new Array(aNumRows);  // initialize array of rows
    for (var r = 0; r < aNumRows; ++r) {
        m[r] = new Array(bNumCols); // initialize the current row
        for (var c = 0; c < bNumCols; ++c) {
            m[r][c] = 0;             // initialize the current cell
            for (var i = 0; i < aNumCols; ++i) {
                m[r][c] += a[r][i] * b[i][c];
            }
        }
    }
    return m;
}

function markCrossHairs(ctx, x, y)
{
    ctx.beginPath();
    ctx.moveTo(x, y - 100);
    ctx.lineTo(x, y + 100);
    ctx.stroke();
    ctx.beginPath();
    ctx.moveTo(x - 100, y);
    ctx.lineTo(x + 100, y);
    ctx.stroke();
}

function drawCircle(ctx, x, y)
{
    ctx.beginPath();
    ctx.arc(x, y, 30, 0, 2 * Math.PI);
    ctx.fill();
}