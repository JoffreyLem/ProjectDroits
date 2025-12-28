import React, { useState, useRef, useEffect } from 'react';
import {
  DroitService,
  PromptContentDto,
  FondApiName,
} from './services/DroitService';
import './index.css';
import { ApiResponseError } from './services/ApiService.ts';

interface Message {
  id: number;
  sender: 'user' | 'bot' | 'error';
  text: string;
}

export enum SearchModeEnum {
  simple = 'simple',
  avance = 'avancé',
}

const availableFonds: FondApiName[] = [
  FondApiName.CODE,
  FondApiName.LEGI,
  FondApiName.KALI,
  FondApiName.JORF,
];

const fondLabels: Record<FondApiName, string> = {
  [FondApiName.CODE]: 'Codes (Lois codifiées)',
  [FondApiName.LEGI]: 'Lois & Règlements (non codifiés)',
  [FondApiName.KALI]: 'Conventions Collectives',
  [FondApiName.JORF]: 'Journal Officiel (Textes récents)',
};

const App = () => {
  const [currentMessage, setCurrentMessage] = useState<string>('');
  const [messages, setMessages] = useState<Message[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [searchMode, setSearchMode] = useState<SearchModeEnum>(
    SearchModeEnum.simple,
  );
  const [selectedFonds, setSelectedFonds] = useState<FondApiName[]>([
    ...availableFonds,
  ]);
  const messagesEndRef = useRef<HTMLDivElement>(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  const handleFondSelectionChange = (fond: FondApiName, isChecked: boolean) => {
    setSelectedFonds((prevFonds) => {
      if (isChecked) {
        return prevFonds.includes(fond) ? prevFonds : [...prevFonds, fond];
      } else {
        return prevFonds.filter((f) => f !== fond);
      }
    });
  };

  const handleSendMessage = async () => {
    const trimmedMessage = currentMessage.trim();
    if (!trimmedMessage || loading) return;

    // 1. Préparer et afficher le message utilisateur (inchangé)
    const userMessage: Message = {
      id: Date.now(),
      sender: 'user',
      text: trimmedMessage,
    };
    // Utilisation de la fonction de mise à jour pour garantir l'état le plus récent
    setMessages((prevMessages) => [...prevMessages, userMessage]);

    const promptText = currentMessage;
    setCurrentMessage('');
    setLoading(true);

    const promptContent: PromptContentDto = {
      content: promptText,
      isAdvancedSearch: searchMode === SearchModeEnum.avance,
      selectedFonds:
        searchMode === SearchModeEnum.avance ? selectedFonds : undefined,
    };

    let result: string | ApiResponseError | null = null;

    try {
      result = await DroitService.GlobalSearch(promptContent);

      const botMessage: Message = {
        id: Date.now() + 1,
        sender: 'bot',
        text: JSON.stringify(result, null, 2),
      };
      setMessages((prevMessages) => [...prevMessages, botMessage]);
    } catch (err: any) {
      console.error('Erreur API:', err);
      const errorText = `Une erreur s'est produite: ${err?.message || 'Erreur inconnue'}`;

      const errorMessage: Message = {
        id: Date.now() + 1,
        sender: 'error',
        text: errorText,
      };
      setMessages((prevMessages) => [...prevMessages, errorMessage]);
    } finally {
      setLoading(false);
    }
  };

  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      handleSendMessage();
    }
  };

  const baseBubbleClasses =
    'max-w-[75%] w-fit py-2 px-4 rounded-xl shadow-sm break-words text-sm leading-snug';

  const getSenderClasses = (sender: Message['sender']): string => {
    switch (sender) {
      case 'user':
        return 'bg-green-200 self-end ml-auto rounded-br-md';
      case 'bot':
        return 'bg-white self-start mr-auto border border-gray-200 rounded-bl-md text-xs';
      case 'error':
        return 'bg-red-100 text-red-700 self-start mr-auto border border-red-300 rounded-bl-md italic';
      default:
        return '';
    }
  };

  const modeButtonBaseClasses =
    'px-4 py-1 text-sm font-medium rounded-md transition-colors duration-150 ease-in-out focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500';
  const activeModeClasses = 'bg-blue-500 text-white';
  const inactiveModeClasses = 'bg-gray-200 text-gray-700 hover:bg-gray-300';

  return (
    <div className="flex h-screen w-full flex-col overflow-hidden bg-gray-100 font-sans">
      {/* Zone des messages */}
      <div className="flex-grow space-y-3 overflow-y-auto p-4">
        {messages.map((msg) => (
          <div
            key={msg.id}
            className={`${baseBubbleClasses} ${getSenderClasses(msg.sender)}`}
          >
            {msg.sender === 'bot' ? (
              <pre className="m-0 whitespace-pre-wrap font-mono">
                {msg.text}
              </pre>
            ) : (
              msg.text
            )}
          </div>
        ))}
        {loading && (
          <div
            className={`${baseBubbleClasses} mr-auto self-start rounded-bl-md border border-gray-200 bg-white italic opacity-70`}
          >
            Traitement en cours...
          </div>
        )}
        <div ref={messagesEndRef} />
      </div>

      {/* Zone de saisie et contrôles */}
      <div className="border-t border-gray-300 bg-white p-3 shadow-[0_-2px_5px_rgba(0,0,0,0.05)]">
        {/* Sélecteur de Mode */}
        <div className="mb-2 flex justify-center space-x-2">
          <button
            onClick={() => setSearchMode(SearchModeEnum.simple)}
            className={`${modeButtonBaseClasses} ${searchMode === SearchModeEnum.simple ? activeModeClasses : inactiveModeClasses}`}
          >
            Recherche Simple
          </button>
          <button
            onClick={() => setSearchMode(SearchModeEnum.avance)}
            className={`${modeButtonBaseClasses} ${searchMode === SearchModeEnum.avance ? activeModeClasses : inactiveModeClasses}`}
          >
            Recherche Avancée
          </button>
        </div>

        {/* Zone pour les contrôles avancés (checkboxes pour les fonds) */}
        {searchMode === 'avancé' && (
          <div className="mb-3 mt-2 rounded-md border border-gray-200 bg-gray-50 p-2">
            <label className="mb-1 block text-center text-xs font-semibold text-gray-600">
              Fonds à inclure dans la recherche :
            </label>
            <div className="flex flex-wrap justify-center gap-x-4 gap-y-1">
              {/* Itération sur l'enum via availableFonds */}
              {availableFonds.map((fond) => (
                <label
                  key={fond} // La clé peut être la valeur de l'enum (qui est une string ici)
                  className="flex cursor-pointer items-center space-x-1 text-xs text-gray-700 hover:text-blue-600"
                >
                  <input
                    type="checkbox"
                    className="h-3.5 w-3.5 cursor-pointer rounded border-gray-300 text-blue-600 focus:ring-blue-500"
                    // La comparaison fonctionne avec les membres de l'enum
                    checked={selectedFonds.includes(fond)}
                    onChange={(e) =>
                      handleFondSelectionChange(fond, e.target.checked)
                    }
                    disabled={loading}
                  />
                  {/* Affichage du libellé User-Friendly via le mapping */}
                  <span>{fondLabels[fond]}</span>
                </label>
              ))}
            </div>
          </div>
        )}

        {/* Barre de saisie et bouton Envoyer */}
        <div className="flex items-center">
          <input
            type="text"
            value={currentMessage}
            onChange={(e) => setCurrentMessage(e.target.value)}
            onKeyPress={handleKeyPress}
            placeholder={
              searchMode === SearchModeEnum.simple
                ? 'Posez votre question...'
                : 'Entrez vos mots-clés...'
            }
            className="mr-2 flex-grow rounded-full border border-gray-300 px-4 py-3 text-base focus:border-transparent focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
            disabled={loading}
          />
          <button
            onClick={handleSendMessage}
            className="cursor-pointer rounded-full bg-blue-500 px-5 py-2.5 text-base font-semibold text-white transition-colors duration-200 ease-in-out hover:bg-blue-600 disabled:cursor-not-allowed disabled:opacity-50"
            disabled={
              loading ||
              !currentMessage.trim() ||
              (searchMode === SearchModeEnum.avance &&
                selectedFonds.length === 0)
            }
          >
            {loading ? '...' : 'Envoyer'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default App;
