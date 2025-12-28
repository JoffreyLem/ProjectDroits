import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

const target = process.env.API_URL || 'https://localhost:7287/';
const secure = process.env.SECURE === 'true';

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    target: 'esnext',
  },
  plugins: [plugin()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    proxy: {
      '^/Api/Droit/': {
        target,
        secure,
        changeOrigin: true,
      },
    },
    port: 54839,
  },
});
