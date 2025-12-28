#!/bin/sh
 
 # Vérifie si le modèle existe déjà
 if ! ollama list | grep -q "mon_modele"; then
     echo "Création du modèle mon_modele..."
     ollama create mon_modele -f /app/Modelfile
 else
     echo "Le modèle mon_modele existe déjà."
 fi
 
 # Lancer Ollama en mode serveur
 exec ollama serve
