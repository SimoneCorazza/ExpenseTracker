import { useCallback, useState } from 'react';
import Account from './pages/Account';
import Calendario from './pages/Calendario';
import { getNomeUtente } from './services/LocalStorage';

const App = () => {
  const [nomeUtenteImpostato, setNomeUtenteImpostato] = useState<boolean>(!!getNomeUtente()); 

  const onNomeAccountImpostato = useCallback(() => {
    setNomeUtenteImpostato(true);
  }, []);

  if (nomeUtenteImpostato) {
    return <Calendario />;
  } else {
    return <Account nomeImpostato={onNomeAccountImpostato} />
  }  
};

export default App;