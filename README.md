# Projet EVHI : CoPillarz

## Table des matières

- [Présentation](#présentation)
- [Installation](#installation)
- [Utilisation](#utilisation)
- [Organisation du dépôt](#organisation-du-dépôt)

## Présentation

Dans ce répertoire, vous trouverez l'implémentation Unity de notre projet pour l'UE EVHI du M2 ANDROIDE.
Les membres de l'équipe sont :

- **BOUTON POPPER Jules**
- **MAIRE Maxence**
- **SUN Amélie**

Notre projet consiste en un jeu de logique, dont le langage est basé sur celui du jeu "The Witness" ([trailer](https://youtu.be/SPMMKFX78x0) du jeu et [page Steam](https://store.steampowered.com/app/210970/The_Witness/)). Dans l'esprit du jeu original, où les mécaniques ne sont pas expliquées au joueur et qu'il doit donc déduire et apprendre à mesure qu'il progresse, nous avons opté pour une modélisation de l'apprenant ainsi que pour du feedback adaptatif. Nous avons choisi comme modalités la VR et l'haptique, dans le cadre du projet [CoVR](https://www.isir.upmc.fr/actualites/a-lisir-le-projet-covr-effleure-le-toucher-du-bout-des-doigts/). L'objectif du projet est de proposer une expérience de jeu immersive, interactive aussi bien dans l'environnement virtuel que dans le monde réel et qui s'adapte à l'apprentissage de l'utilisateur.

## Installation

Le projet est conçu comme un sous module du projet CoVR. Avant toutes choses, il est donc nécessaire de cloner le projet CoVR sur lequel notre projet est basé. Pour cela, exécutez les commandes suivantes :

```bash
git clone https://gitlab.isir.upmc.fr/stech/covr/xr_modules/parentwithsubmodule.git --recurse-submodules
cd parentwithsubmodule/Assets/CoVrToolKit/
```

Notre projet n'est pas intégré dans le projet CoVR, il faut donc le cloner à part :

```bash
cd parentwithsubmodule/Assets/demoScene
git clone https://github.com/Maxenceoresteleandre/LAByrinth_submodule.git --recurse-submodules
```

Il est également recommandé de renommer le dossier `LAByrinth_submodule` en `LAByrinth` pour éviter tout problème.

## Utilisation

Pour lancer le projet, il vous faudra avoir Unity installé sur votre machine. Ouvrez Unity Hub, cliquez sur "Add -> Add project from disk"  et sélectionnez le dossier `parentwithsubmodule`. Le projet doit être ouvert avec la version 2020.3.19f1 de l'éditeur Unity. La première fois que vous ouvrirez le projet, Unity vous fera les importations et la serialization des assets, cela peut prendre un certain temps. Une fois que c'est fait, vous pouvez ouvrir la scène `Assets/CoVrToolKit/CoVRIntegration/Persistence.unity`, c'est la scène qui représente virtuellement la plateforme CoVR. Il faut également modifier dans `Assets\demoScene\LAByrinth\script\WherePython.cs` la variable `pythonPath` pour qu'elle pointe vers votre installation de Python. Enfin, il faut installer les dépendances Python pour le projet LAByrinth. Pour cela, exécutez la commande suivante :

```bash
pip install pyBKT
```

En mode "Play", l'opérateur peur selectionner Scene Loader et lancer LAByrinth.

## Organisation du dépôt

Ce dépôt est contient le projet Unity de notre jeu. Il est composé de ce dossier ainsi que de plusieurs sous-modules :

- [CoPillars-model](https://github.com/Ameliouse/CoPillars-model.git) : le modèle BKT
- [LevelGenerator](https://github.com/b0gz1b/LevelGenerator.git) : Le moteur logique du jeu et le générateur de niveaux

(De préférence il faut checkout sur la branche `main` pour chaque sous-module)
