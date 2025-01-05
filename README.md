# Gim-CaseStudy
Gimica/JustPlay CaseStudy for the Sn. Unity Game Developer role.

## General Info
- <b>Development Hours</b>: ~6h
- <b>Engine</b>: Unity3D
- <b>Editor Version</b>: 2022.3.37f1

## Imported Assets
- <a href="https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/index.html">TextMeshPro</a>
- <a href="https://docs.unity3d.com/Packages/com.unity.2d.sprite@1.0/manual/index.html">SpriteEditor</a>

## Rundown
Each problem is solved in a separate scene: Problem1, Problem2, and Problem3. They have their own namespace and folder structure to keep things organized.

- <b>Problem1</b>: Mover.cs moves the attached GameObject toward its target at a constant speed. It includes optional inspector parameters for speed and look-at behavior (to make the GameObject face the target as it moves).

![9fp8t1](https://github.com/user-attachments/assets/ee56196b-2f94-48e0-9b7b-b03f478dcbd6)

- <b>Problem2</b>: 
  - The Problem2 UI follows the MVVM pattern. Each view is controlled through a ViewModel interface, acting as an intermediary between the front end and the underlying data. The ViewModel retrieves business logic from Models, ensuring communication between the UI and the game's data.
  - A simple version of <b>ReactiveProperty.cs</b> is implementated as an Observer pattern for handling property changes reactively.

All of the settings regarding to the currencies such as 'AddResourceAmount', 'CurrencyColor', 'CurrencyIcon', are stored in <b>CurrencyData ScriptableObject</b> (Assets > ScriptableObjects > Problem2).

![9fp940](https://github.com/user-attachments/assets/b73d8e77-15d5-4875-84c2-2735c5803979)

- <b>Problem3</b>: ProblemThree calculates the maximum possible profit based on the given stock prices and constraints.To start the calculation, press the "Calculate Max Profit" button at runtime. The results will be displayed in the screen space.

You can modify the Stock Prices directly through the Inspector window in ProblemThreeManager.cs.

![9fp9hf](https://github.com/user-attachments/assets/b331de79-ea3e-4edb-bb7c-8af4e6e821e7)
