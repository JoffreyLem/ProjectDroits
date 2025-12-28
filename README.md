# ProjectDroit - Assistant Juridique IA 🤖⚖️

> ⚠️ **Projet Expérimental** - Ce projet a été développé dans le cadre d'une expérimentation visant à tester les capacités des modèles d'IA (LLM) appliqués au domaine juridique français.

## 📝 Description

ProjectDroit est une application expérimentale d'assistance juridique qui combine l'intelligence artificielle avec l'API Legifrance pour analyser et répondre à des questions juridiques. Le projet utilise des modèles de langage (LLM) pour extraire des mots-clés à partir de questions en langage naturel, rechercher dans la base de données législative française, et générer des réponses contextualisées.

### Objectifs de l'expérimentation

- Tester l'efficacité des LLM (Ollama et Gemini) pour comprendre des questions juridiques
- Évaluer la pertinence de l'extraction automatique de mots-clés juridiques
- Expérimenter l'intégration d'une IA avec l'API officielle Legifrance
- Développer une architecture clean code avec .NET et React

## 🏗️ Architecture

### Stack Technique

**Backend (.NET 8)**
- ASP.NET Core Web API
- Architecture en couches (Clean Architecture)
  - `ProjectDroit.Server` - API REST
  - `ProjectDroit.Core` - Logique métier et cas d'usage
  - `ProjectDroit.Domain` - Entités et modèles de domaine
  - `ProjectDroit.Infrastructure` - Intégrations externes (Legifrance, LLM)

**Frontend (React + TypeScript)**
- React 18
- TypeScript
- Vite (build tool)
- Tailwind CSS
- React Router
- Axios

**Infrastructure**
- Docker Compose
- Ollama (LLM local)
- Seq (Logging)
- MongoDB (Base de données)

## 🚀 Fonctionnalités

### Recherche Globale
L'application analyse une question en langage naturel, extrait les mots-clés pertinents via IA, recherche dans Legifrance et génère une réponse contextuelle.

**Workflow:**
1. L'utilisateur pose une question juridique
2. Le LLM extrait les mots-clés juridiques pertinents
3. Recherche dans l'API Legifrance avec ces mots-clés
4. Récupération du texte intégral des articles de loi
5. Génération d'une réponse synthétique par le LLM

### Recherche Avancée
Permet de cibler des fonds juridiques spécifiques (Code civil, Code pénal, etc.) pour des recherches plus précises.

## 📋 Prérequis

- .NET 8 SDK
- Node.js 18+
- Docker et Docker Compose
- Clés API:
  - Legifrance API (OAuth2)
  - Google Gemini API (optionnel)

## 🛠️ Installation

### 1. Cloner le projet

```bash
git clone <repository-url>
cd projectdroitapp
```

### 2. Configuration des variables d'environnement

Créer un fichier `.env` à la racine du projet:

```env
# Legifrance API
LEGIFRANCE_CLIENT_ID=votre_client_id
LEGIFRANCE_CLIENT_SECRET=votre_client_secret

# Gemini API (optionnel)
GEMINI_API_KEY=votre_gemini_api_key

# Seq (Logging)
SEQ_URL=http://localhost:8080
SEQ_APIKEY=votre_seq_apikey
```

### 3. Démarrer l'infrastructure Docker

```bash
docker-compose -f docker-compose.dev.yml up -d
```

Cela démarrera:
- MongoDB (port 27017)
- Seq pour les logs (port 8080)
- Ollama pour le LLM local (port 11434)

### 4. Installer et configurer Ollama

```bash
# Télécharger le modèle llama3.2-3b
docker exec -it ollama ollama pull llama3.2:3b
```

### 5. Installation du backend

```bash
cd ProjectDroit.Server
dotnet restore
dotnet run
```

L'API sera disponible sur `https://localhost:7141`

### 6. Installation du frontend

```bash
cd projectdroit.client
npm install
npm run dev
```

L'interface sera disponible sur `https://localhost:5173`

## 📚 Structure du Projet

```
projectdroitapp/
├── ProjectDroit.Server/          # API REST
│   ├── Controllers/              # Endpoints API
│   ├── Dto/                      # Data Transfer Objects
│   └── Program.cs                # Configuration de l'application
├── ProjectDroit.Core/            # Couche métier
│   ├── UseCases/                 # Logique métier
│   ├── Interfaces/               # Contrats
│   └── Dto/                      # DTOs partagés
├── ProjectDroit.Domain/          # Entités du domaine
│   └── Entities/                 # Modèles de données
├── ProjectDroit.Infrastructure/  # Services externes
│   └── Http/
│       ├── Legifrance/           # Client API Legifrance
│       ├── Ollama/               # Client Ollama
│       └── Gemini/               # Client Gemini
├── projectdroit.client/          # Application React
│   ├── src/
│   │   ├── components/           # Composants React
│   │   └── services/             # Services API
│   └── public/                   # Assets statiques
└── docker-compose.dev.yml        # Configuration Docker
```

## 🔑 API Endpoints

### POST `/Api/Droit/SearchLaw`

Recherche et analyse de questions juridiques.

**Request Body:**
```json
{
  "content": "Quelle est la majorité en France ?",
  "isAdvancedSearch": false,
  "selectedFonds": []
}
```

**Response:**
```json
{
  "response": "D'après le Code civil français, la majorité est fixée à 18 ans..."
}
```

## 🧪 Technologies Expérimentées

### Modèles LLM Testés

1. **Ollama (llama3.2:3b)**
   - Hébergement local
   - Latence réduite
   - Confidentialité des données

2. **Google Gemini**
   - API cloud
   - Performances supérieures
   - Coût par requête

### Patterns Architecturaux

- **Clean Architecture** - Séparation claire des responsabilités
- **Repository Pattern** - Abstraction de l'accès aux données externes
- **Use Case Pattern** - Logique métier encapsulée
- **Dependency Injection** - Couplage faible entre les composants

## 📊 Logging et Monitoring

Le projet utilise **Serilog** avec **Seq** pour un logging structuré:

- Accès Seq: `http://localhost:8080`
- Username: admin
- Password: admin

## ⚠️ Limitations et Avertissements

Ce projet est **strictement expérimental** et ne doit pas être utilisé pour:
- Des conseils juridiques officiels
- Des décisions juridiques importantes
- Une utilisation en production

**Limitations connues:**
- Les réponses de l'IA peuvent contenir des inexactitudes
- La base de données Legifrance n'est pas exhaustive via l'API publique
- Les performances dépendent du modèle LLM utilisé
- Pas de validation juridique professionnelle des réponses

## 🔮 Pistes d'Amélioration

- [ ] Fine-tuning d'un modèle spécialisé en droit français
- [ ] Mise en cache des résultats de recherche Legifrance
- [ ] Interface de feedback pour améliorer les réponses
- [ ] Support de documents juridiques externes (PDF, DOCX)
- [ ] Historique des conversations
- [ ] Export des réponses en PDF
- [ ] Tests unitaires et d'intégration plus complets

## 📄 License

Ce projet est à des fins éducatives et expérimentales uniquement.

## 🤝 Contributions

Ce projet est une expérimentation personnelle et n'accepte pas de contributions externes pour le moment.

## 📞 Contact

Pour toute question sur l'expérimentation ou l'architecture technique, n'hésitez pas à ouvrir une issue.

---

**Rappel:** Ce projet est une expérimentation technique pour explorer les capacités des LLMs dans le domaine juridique. Il ne remplace en aucun cas l'avis d'un professionnel du droit.
