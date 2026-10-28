<script setup lang="ts">
import { ref } from 'vue'
import {
  HeartPulse,
  LayoutDashboard,
  FileText,
  ShoppingCart,
  Users,
  Settings,
  Bell,
  Search,
  Menu,
  X,
} from 'lucide-vue-next'

const sidebarOpen = ref(false)

const navItems = [
  { label: 'Dashboard', icon: LayoutDashboard, active: false },
  { label: 'Purchase Requests', icon: FileText, active: false },
  { label: 'Purchase Orders', icon: ShoppingCart, active: true },
  { label: 'Suppliers', icon: Users, active: false },
  { label: 'Settings', icon: Settings, active: false },
]
</script>

<template>
  <div class="min-h-screen bg-slate-50 flex">
    <!-- Sidebar -->
    <aside
      :class="[
        'fixed lg:static inset-y-0 left-0 z-40 w-64 bg-slate-900 text-slate-300 flex flex-col transition-transform duration-300',
        sidebarOpen ? 'translate-x-0' : '-translate-x-full lg:translate-x-0',
      ]"
    >
      <!-- Brand -->
      <div class="flex items-center gap-3 px-5 h-16 border-b border-slate-800">
        <div class="flex items-center justify-center w-9 h-9 rounded-lg bg-emerald-500 text-white">
          <HeartPulse :size="20" />
        </div>
        <div class="leading-tight">
          <p class="text-white font-bold text-sm tracking-wide">DRMC</p>
          <p class="text-slate-400 text-[10px] uppercase tracking-wider">Procurement</p>
        </div>
        <button class="ml-auto lg:hidden text-slate-400" @click="sidebarOpen = false">
          <X :size="20" />
        </button>
      </div>

      <!-- Nav -->
      <nav class="flex-1 px-3 py-4 space-y-1">
        <p class="px-3 text-[10px] font-semibold text-slate-500 uppercase tracking-wider mb-2">Main Menu</p>
        <a
          v-for="item in navItems"
          :key="item.label"
          href="#"
          :class="[
            'flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm font-medium transition-colors',
            item.active
              ? 'bg-brand-600 text-white'
              : 'text-slate-400 hover:text-white hover:bg-slate-800',
          ]"
        >
          <component :is="item.icon" :size="18" />
          {{ item.label }}
        </a>
      </nav>

      <!-- Footer -->
      <div class="px-4 py-4 border-t border-slate-800">
        <p class="text-[10px] text-slate-500">Davao Regional Medical Center</p>
        <p class="text-[10px] text-slate-600">Tagum City, Davao del Norte</p>
      </div>
    </aside>

    <!-- Overlay -->
    <div v-if="sidebarOpen" class="fixed inset-0 z-30 bg-slate-900/40 lg:hidden" @click="sidebarOpen = false" />

    <!-- Main -->
    <div class="flex-1 flex flex-col min-w-0">
      <!-- Topbar -->
      <header class="h-16 bg-white border-b border-slate-200 flex items-center px-4 lg:px-6 gap-4 sticky top-0 z-20">
        <button class="lg:hidden text-slate-600" @click="sidebarOpen = true">
          <Menu :size="22" />
        </button>

        <div class="relative hidden md:block flex-1 max-w-md">
          <Search :size="16" class="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
          <input
            type="text"
            placeholder="Search purchase orders, suppliers..."
            class="w-full pl-9 pr-4 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
          />
        </div>

        <div class="ml-auto flex items-center gap-3">
          <button class="relative text-slate-500 hover:text-slate-700 p-2 rounded-lg hover:bg-slate-100 transition-colors">
            <Bell :size="20" />
            <span class="absolute top-1.5 right-1.5 w-2 h-2 bg-red-500 rounded-full ring-2 ring-white"></span>
          </button>
          <div class="flex items-center gap-3 pl-3 border-l border-slate-200">
            <div class="text-right hidden sm:block">
              <p class="text-sm font-semibold text-slate-800">Maria Santos</p>
              <p class="text-xs text-slate-500">BAC Secretariat</p>
            </div>
            <div class="w-9 h-9 rounded-full bg-brand-100 text-brand-700 flex items-center justify-center text-sm font-semibold">
              MS
            </div>
          </div>
        </div>
      </header>

      <!-- Viewport -->
      <main class="flex-1 p-4 lg:p-8 overflow-x-hidden">
        <slot />
      </main>
    </div>
  </div>
</template>
