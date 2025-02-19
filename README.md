# 🍃 Natural Healing - AR Project (Code Only)  

**Natural Healing** is an **Augmented Reality (AR) experience** designed to explore the connection between **nature and mental well-being**.  
This repository contains **only the code** used in the project, while all assets (models, textures, sounds, etc.) have been excluded to keep the repository lightweight.  

📺 **Project Demo**: [Watch on YouTube](https://www.youtube.com/shorts/m8Ze0FsmMs0)  
[![Watch the video](https://img.youtube.com/vi/m8Ze0FsmMs0/0.jpg)](https://www.youtube.com/shorts/m8Ze0FsmMs0)
🔗 **Full Project Portfolio**: [My AR Project Overview](https://lyk953176376.wixsite.com/my-site-3)  

---

## **🌿 Core Features**  

### 🔹 **1. AR Object Generation**  
- Uses **AR Foundation’s plane detection** to generate objects dynamically.  
- A **trigger is attached to the user's hand**, linking to the `ARContactSpawnTrigger` script.  
- Triggers object spawning and removes user guidelines based on the detected plane type.  

### 🔹 **2. Dynamic Object Spawning**  
- `ObjectSpawner` script handles **automatic object placement** based on surface type (horizontal/vertical).  
- Spawns objects from predefined prefab lists, aligning them correctly to surfaces.  
- Supports **growth animations** and **sound effects** for enhanced interactivity.  

### 🔹 **3. Growth Animation System**  
- `FlowerControlWithMPB` script manages plant growth using **MaterialPropertyBlock (MPB)**.  
- Ensures **efficient material updates** without affecting shared materials.  
- Growth starts when triggered and stops upon reaching the target state.  

### 🔹 **4. Interactive Mushroom Effects**  
- `MushroomGrabEffect` script **removes visual effects** when a mushroom is grabbed.  
- `MushroomGrab` script **plays a sound effect**, ensuring it only triggers once.  

### 🔹 **5. In-Game Debug System**  
- Since **console debugging is not visible in AR**, `DebugDisplay` script **shows debug messages on UI**.  
- Implements a **singleton pattern**, ensuring a single instance manages debug logs efficiently.  

---

## **🛠 Development**  
- **Engine**: Unity  
- **AR Framework**: AR Foundation  
- **Programming Language**: C#  
- **Project Type**: Team Project (5 members)  
- **My Role**: **Developer (All programming & AR interactions)**  

---

## **👨‍💻 My Contributions**  
I was the **sole programmer** for this project, responsible for:  
✔ **Developing all AR interactions and object spawning logic**  
✔ **Implementing dynamic material updates with MPB for animations**  
✔ **Optimizing user interactions & performance for mobile AR**  
✔ **Creating a custom in-game debugging system**  

🔗 **More details about my contributions & project overview:**  
👉 [Natural Healing - AR Project](https://lyk953176376.wixsite.com/my-site-3)  
