import eslintPluginReact from "eslint-plugin-react";
import eslintPluginReactHooks from "eslint-plugin-react-hooks";
import eslintPluginReactRefresh from "eslint-plugin-react-refresh";
import eslintPluginPrettier from "eslint-plugin-prettier";
import eslintPluginTS from "@typescript-eslint/eslint-plugin";
import tsParser from "@typescript-eslint/parser";
import prettierConfig from "eslint-config-prettier";

export default [
  {
    files: ["**/*.{ts,tsx,js,jsx}"], // Cible tous les fichiers JS/TS
    languageOptions: {
      parser: tsParser,
      ecmaVersion: "latest",
      sourceType: "module"
    },
    plugins: {
      react: eslintPluginReact,
      "react-hooks": eslintPluginReactHooks,
      "react-refresh": eslintPluginReactRefresh,
      "@typescript-eslint": eslintPluginTS,
      prettier: eslintPluginPrettier
    },
    rules: {
      "prettier/prettier": "error", // Erreur si le formatage Prettier n'est pas respecté
      "react/react-in-jsx-scope": "off", // Plus nécessaire avec React 17+
      "react-hooks/rules-of-hooks": "error", // Vérifie l'usage correct des Hooks
      "react-hooks/exhaustive-deps": "warn", // Avertit sur les dépendances des Hooks
      "react-refresh/only-export-components": "warn", // Assure le bon fonctionnement de React Fast Refresh
      "@typescript-eslint/explicit-module-boundary-types": "off" // Désactive l'obligation de typer les retours de fonctions
    },
    settings: {
      react: {
        version: "detect" // Détecte automatiquement la version de React
      }
    },
    extends: [
      eslintPluginReact.configs.recommended, // Règles recommandées pour React
      eslintPluginReactHooks.configs.recommended, // Règles recommandées pour les Hooks
      eslintPluginTS.configs.recommended, // Règles TypeScript
      prettierConfig // Désactive les règles ESLint qui pourraient entrer en conflit avec Prettier
    ]
  }
];