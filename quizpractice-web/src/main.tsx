import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { App as AntApp, ConfigProvider } from 'antd'
import enUS from 'antd/locale/en_US'
import { isAxiosError } from 'axios'
import { RouterProvider } from 'react-router/dom'
import { router } from './routes/router'
import './index.css'

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      // Lists are reloaded on purpose after a change (invalidateQueries); no need to
      // refetch every time the window gets focus again.
      refetchOnWindowFocus: false,
      // Retry once when the server could not answer; a 4xx answer (not found,
      // invalid, forbidden) would only fail the same way again.
      retry: (failures, error) => failures < 1 && !(isAxiosError(error) && (error.response?.status ?? 500) < 500),
    },
  },
})

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      {/* The screens of the SRS are in English (part III wireframes). */}
      <ConfigProvider locale={enUS}>
        <AntApp>
          <RouterProvider router={router} />
        </AntApp>
      </ConfigProvider>
    </QueryClientProvider>
  </StrictMode>,
)
