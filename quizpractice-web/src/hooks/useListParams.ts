import { useCallback, useMemo } from 'react'
import { useSearchParams } from 'react-router'
import type { ListCriteria } from '../api/types'

/** SRS III 0.3: a list shows ten rows per page. */
export const PAGE_SIZE = 10

/**
 * Keeps the criteria of a list screen in the address (?search=…&page=2), so Back from
 * a detail screen, a reload or a shared link shows the same list. Changing the search,
 * a filter or the order returns to page 1 (SRS III 0.3).
 *
 * Filters a screen adds (status, category…) are read with get() and set with set().
 */
export function useListParams() {
  const [params, setParams] = useSearchParams()

  const criteria: ListCriteria = useMemo(
    () => ({
      page: Number(params.get('page') ?? 1),
      pageSize: PAGE_SIZE,
      search: params.get('search') ?? undefined,
      sortBy: params.get('sortBy') ?? undefined,
      descending: params.get('descending') === 'true' || undefined,
    }),
    [params],
  )

  const get = useCallback((key: string) => params.get(key) ?? undefined, [params])

  /** Sets criteria; any change other than the page itself goes back to page 1. */
  const set = useCallback(
    (changes: Record<string, string | number | boolean | undefined>) => {
      setParams((current) => {
        const next = new URLSearchParams(current)
        for (const [key, value] of Object.entries(changes)) {
          if (value === undefined || value === '' || value === false) next.delete(key)
          else next.set(key, String(value))
        }
        if (!('page' in changes)) next.delete('page')
        return next
      })
    },
    [setParams],
  )

  const clear = useCallback(() => setParams(new URLSearchParams()), [setParams])

  return { criteria, get, set, clear, hasCriteria: params.size > 0 }
}
