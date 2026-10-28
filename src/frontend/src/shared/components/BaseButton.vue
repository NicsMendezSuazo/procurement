<script setup lang="ts">
withDefaults(
  defineProps<{
    variant?: 'primary' | 'secondary' | 'ghost' | 'danger' | 'success'
    size?: 'sm' | 'md' | 'lg'
    disabled?: boolean
    type?: 'button' | 'submit' | 'reset'
  }>(),
  {
    variant: 'primary',
    size: 'md',
    disabled: false,
    type: 'button',
  },
)
</script>

<template>
  <button
    :type="type"
    :disabled="disabled"
    :class="[
      'inline-flex items-center justify-center gap-2 font-medium rounded-lg transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-offset-1 disabled:opacity-50 disabled:cursor-not-allowed',
      {
        'bg-brand-600 text-white hover:bg-brand-700 focus:ring-brand-500 shadow-sm': variant === 'primary',
        'bg-white text-slate-700 border border-slate-300 hover:bg-slate-50 focus:ring-brand-400': variant === 'secondary',
        'text-slate-600 hover:bg-slate-100 focus:ring-slate-300': variant === 'ghost',
        'bg-red-600 text-white hover:bg-red-700 focus:ring-red-400 shadow-sm': variant === 'danger',
        'bg-emerald-600 text-white hover:bg-emerald-700 focus:ring-emerald-400 shadow-sm': variant === 'success',
      },
      {
        'px-3 py-1.5 text-sm': size === 'sm',
        'px-4 py-2 text-sm': size === 'md',
        'px-5 py-2.5 text-base': size === 'lg',
      },
    ]"
  >
    <slot />
  </button>
</template>
