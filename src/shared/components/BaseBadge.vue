<script setup lang="ts">
withDefaults(
  defineProps<{
    variant?: 'gray' | 'blue' | 'amber' | 'emerald' | 'red'
    size?: 'sm' | 'md'
  }>(),
  {
    variant: 'gray',
    size: 'sm',
  },
)
</script>

<template>
  <span
    :class="[
      'inline-flex items-center gap-1.5 font-medium rounded-full whitespace-nowrap',
      {
        'bg-slate-100 text-slate-700': variant === 'gray',
        'bg-blue-100 text-blue-700': variant === 'blue',
        'bg-amber-100 text-amber-800': variant === 'amber',
        'bg-emerald-100 text-emerald-700': variant === 'emerald',
        'bg-red-100 text-red-700': variant === 'red',
      },
      {
        'px-2.5 py-0.5 text-xs': size === 'sm',
        'px-3 py-1 text-sm': size === 'md',
      },
    ]"
  >
    <slot />
  </span>
</template>
