import React from 'react';
import ReactDOM from "react-dom/client";
import App from './App';
import { Settings } from 'luxon';
import { ConfigProvider, theme } from 'antd';

Settings.defaultLocale = "it";

const rootElement = document.getElementById("root") as HTMLElement;
const root = ReactDOM.createRoot(rootElement);

const algoritmi = [];
if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
  algoritmi.push(theme.darkAlgorithm);
} else {
  algoritmi.push(theme.defaultAlgorithm);
}



root.render(
  <React.StrictMode>
    <ConfigProvider
      theme={{
        algorithm: algoritmi,
      }}
    >
      <App/>
    </ConfigProvider>
  </React.StrictMode>
);