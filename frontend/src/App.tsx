import { useCallback, useState } from 'react';

const App = () => {
  const [count, setCount] = useState(0);

  const increment = useCallback(() => {
    setCount((prevCount) => prevCount + 1);
  }, []);

  return (
    <div className="App">
      <h1>Counter: {count}</h1>
      <button onClick={increment}>Increment</button>
    </div>
  );
};

export default App;